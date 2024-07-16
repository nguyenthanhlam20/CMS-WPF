
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Authen
{
    public class AuthenRepository : IAuthenRepository
    {
        public async Task<bool> Login(string username, string password)
        {
            using (var _context = new CourseManagementDBContext())
            {
                return await _context.Students.AnyAsync(x => x.Name == username && password == username);
            }
        }
    }
}
