
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class StudentRepository : IStudentRepository
    {

        public async Task AddNew(Student item)
        {
            try
            {
                using var _context = new CourseManagementDBContext();
                var nextId = _context.Students.Max(c => c.Id);
                item.Id = nextId + 1;
                _context.Students.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        public async Task<List<Student>> GetAll()
        {
            using var _context = new CourseManagementDBContext();
            return await _context.Students.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task Update(Student item)
        {
            try
            {
                using var _context = new CourseManagementDBContext();
                var exist = await _context.Students.FirstOrDefaultAsync(x => x.Id == item.Id);
                if (exist != null)
                {
                    exist.Name = item.Name;
                    exist.Address = item.Address;
                    exist.City = item.City;
                    exist.Department = item.Department;
                    exist.Birthdate = item.Birthdate;
                    exist.Gender = item.Gender;
                    if (!string.IsNullOrEmpty(item.Country)) exist.Country = item.Country;
                    _context.Students.Update(exist);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
