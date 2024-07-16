
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public async Task AddNew(Course item)
        {
            try
            {
                using (var _context = new CourseManagementDBContext())
                {
                    var nextId = _context.Courses.Max(c => c.Id);
                    item.Id = nextId + 1;
                    _context.Courses.Add(item);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }
        }

        public async Task<List<Course>> GetAll()
        {
            using var _context = new CourseManagementDBContext();
            return await _context.Courses.Include(x => x.Enrollments).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task Update(Course item)
        {
            try
            {
                using var _context = new CourseManagementDBContext();
                var exist = await _context.Courses.FirstOrDefaultAsync(x => x.Id == item.Id);
                if (exist != null)
                {
                    exist.Code = item.Code;
                    exist.Title = item.Title;
                    exist.Credits = item.Credits;
                    _context.Courses.Update(exist);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
