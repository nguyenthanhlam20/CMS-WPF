
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class SemesterRepository : ISemesterRepository
    {
        public async Task AddNew(Semester item)
        {
            using (var _context = new CourseManagementDBContext())
            {
                var nextId = _context.Semesters.Max(c => c.Id);
                item.Id = nextId + 1;
                _context.Semesters.Add(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Semester>> GetAll()
        {
            using var _context = new CourseManagementDBContext();
            return await _context.Semesters.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task Update(Semester item)
        {
            using var _context = new CourseManagementDBContext();
            var exist = await _context.Semesters.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (exist != null)
            {
                exist.Code = item.Code;
                exist.Year = item.Year;
                exist.BeginDate = item.BeginDate;
                exist.EndDate = item.EndDate;
               
                _context.Semesters.Update(exist);
                await _context.SaveChangesAsync();
            }
        }
    }
}
