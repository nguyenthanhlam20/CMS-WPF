using DataAccess.Models;

namespace Repositories
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAll();
        Task AddNew(Course item);
        Task Update(Course item);
    }
}
