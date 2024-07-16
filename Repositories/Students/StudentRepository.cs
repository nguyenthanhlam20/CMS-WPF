
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class StudentRepository : IStudentRepository
    {

        public async Task AddNew(Student item)
        {
            using var _context = new CourseManagementDBContext();
            _context.Students.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAll()
        {
            using var _context = new CourseManagementDBContext();
            return await _context.Students.ToListAsync();
        }

        public async Task Update(Student item)
        {
            using var _context = new CourseManagementDBContext();
            var exist = await _context.Students.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (exist != null)
            {
                exist.Name = item.Name;
                exist.Address = item.Address;
                exist.City = item.City;
                exist.Country = item.Country;
                exist.Birthdate = item.Birthdate;
                _context.Students.Update(exist);
                await _context.SaveChangesAsync();
            }
        }
    }
}
