
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public async Task AddNew(Department item)
        {
            try
            {
                using (var _context = new CourseManagementDBContext())
                {
                    _context.Departments.Add(item);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception)
            {

              
            }
        }

        public async Task<List<Department>> GetAll()
        {
            using var _context = new CourseManagementDBContext();
            return await _context.Departments.OrderByDescending(x => x.Code).ToListAsync();
        }

        public async Task Update(Department item)
        {
            try
            {
                using var _context = new CourseManagementDBContext();
                var exist = await _context.Departments.FirstOrDefaultAsync(x => x.Code == item.Code);
                if (exist != null)
                {
                    exist.Name = item.Name;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
