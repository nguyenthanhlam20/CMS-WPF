using DataAccess.Models;

namespace Services
{
    public interface ISemesterService
    {
        Task<List<Semester>> GetAll();
        Task AddNew(Semester item);
        Task Update(Semester item);
    }
}
