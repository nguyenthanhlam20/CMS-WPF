using DataAccess.Models;

namespace Repositories
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetAll();
        Task AddNew(Enrollment item);
        Task Update(Enrollment item);
    }
}
