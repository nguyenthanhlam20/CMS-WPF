using DataAccess.Models;

namespace Services
{
    public interface IMarkService
    {
        Task<List<Mark>> GetAll();
        Task AddNew(Mark item);
        Task Update(Mark item);
    }
}
