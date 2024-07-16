using DataAccess.Models;

namespace Services
{
    public interface IAssessmentService
    {
        Task<List<Assessment>> GetAll();
        Task AddNew(Assessment item);
        Task Update(Assessment item);
    }
}
