using DataAccess.Models;

namespace Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAll();
        Task AddNew(Student item);
        Task Update(Student item);
    }
}
