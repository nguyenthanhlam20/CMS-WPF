using DataAccess.Models;

namespace Services
{
    public interface IEnrollmentService
    {
        Task<List<Enrollment>> GetAll();
        Task AddNew(Enrollment item);
        Task Update(Enrollment item);
    }
}
