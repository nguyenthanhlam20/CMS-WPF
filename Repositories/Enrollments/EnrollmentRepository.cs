
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        public async Task AddNew(Enrollment item)
        {
            try
            {
                using (var _context = new CourseManagementDBContext())
                {
                    var nextId = _context.Enrollments.Max(c => c.EnrollmentId);
                    item.EnrollmentId = nextId + 1;
                    _context.Enrollments.Add(item);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                
            }
        }

        public async Task<List<Enrollment>> GetAll()
        {
            using var _context = new CourseManagementDBContext();
            return await _context.Enrollments
                .Include(x => x.Student)
                .Include(x => x.Semester)
                .Include(x => x.Course)
                .OrderByDescending(x => x.EnrollmentId)
                .ToListAsync();
        }

        public async Task Update(Enrollment item)
        {
            try
            {
                using var _context = new CourseManagementDBContext();
                var exist = await _context.Enrollments.FirstOrDefaultAsync(x => x.EnrollmentId == item.EnrollmentId);
                if (exist != null)
                {
                    exist.CourseId = item.CourseId;
                    exist.StudentId = item.StudentId;
                    exist.SemesterId = item.SemesterId;

                    _context.Enrollments.Update(exist);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
