
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Repositories
{
    public class AssessmentRepository : IAssessmentRepository
    {
        public async Task AddNew(Assessment item)
        {
            try
            {
                using (var _context = new CourseManagementDBContext())
                {
                    var nextId = _context.Assessments.Max(c => c.Id);
                    item.Id = nextId + 1;
                    _context.Assessments.Add(item);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }
        }

        public async Task<List<Assessment>> GetAll()
        {
            using var _context = new CourseManagementDBContext();
            return await _context.Assessments
                .Include(x => x.Course)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task Update(Assessment item)
        {
            try
            {
                using var _context = new CourseManagementDBContext();
                var exist = await _context.Assessments.FirstOrDefaultAsync(x => x.Id == item.Id);
                if (exist != null)
                {
                    exist.Type = item.Type;
                    exist.Name = item.Name;
                    exist.Percent = item.Percent;
                    exist.CourseId = item.CourseId;

                    _context.Assessments.Update(exist);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
