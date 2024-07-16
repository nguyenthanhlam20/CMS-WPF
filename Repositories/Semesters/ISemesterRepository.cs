using DataAccess.Models;

namespace Repositories
{
    public interface ISemesterRepository
    {
        Task<List<Semester>> GetAll();
        Task AddNew(Semester item);
        Task Update(Semester item);
    }
}
