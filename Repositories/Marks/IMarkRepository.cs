using DataAccess.Models;

namespace Repositories
{
    public interface IMarkRepository
    {
        Task<List<Mark>> GetAll();
        Task AddNew(Mark item);
        Task Update(Mark item);
    }
}
