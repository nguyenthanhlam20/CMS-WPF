using DataAccess.Models;

namespace Services
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAll();
        Task AddNew(Department item);
        Task Update(Department item);
    }
}
