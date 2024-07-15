using DataAccess.Models;

namespace Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAll();
        Task AddNew(Student item);
        Task Update(Student item);
    }
}
