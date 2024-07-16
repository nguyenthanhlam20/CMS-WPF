using DataAccess.Models;

namespace Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetAll();
        Task AddNew(Course item);
        Task Update(Course item);
    }
}
