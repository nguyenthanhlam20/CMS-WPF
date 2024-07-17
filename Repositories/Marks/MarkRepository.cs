
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class MarkRepository : IMarkRepository
    {
        public async Task AddNew(Mark item)
        {
            try
            {
                using (var _context = new CourseManagementDBContext())
                {
                    _context.Marks.Add(item);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<List<Mark>> GetAll()
        {
            using var _context = new CourseManagementDBContext();
            var data = new List<Mark>();
            var marks = await _context.Marks.OrderByDescending(x => x.EnrollmentId).ToListAsync();
            foreach (var mark in marks)
            {

                mark.Assessment = await _context.Assessments
                        .Include(x => x.Course)
                        .FirstOrDefaultAsync(x => x.Id == mark.AssessmentId) ?? new Assessment();

                mark.Enrollment = await _context.Enrollments
                    .Include(x => x.Course)
                    .Include(x => x.Student)
                    .Include(x => x.Semester)
                    .FirstOrDefaultAsync(x => x.EnrollmentId == mark.EnrollmentId) ?? new Enrollment();

                data.Add(mark);
            }

            return data;
        }

        public async Task Update(Mark item)
        {
            try
            {
                using var _context = new CourseManagementDBContext();
                var exist = await _context.Marks
                    .FirstOrDefaultAsync(x => x.EnrollmentId == item.EnrollmentId
                    && x.AssessmentId == item.AssessmentId);
                if (exist != null)
                {
                    exist.Mark1 = item.Mark1;
                    _context.Marks.Update(exist);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
