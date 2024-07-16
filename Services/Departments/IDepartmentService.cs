using DataAccess.Models;

namespace Services
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAll();
        Task AddNew(Department item);
        Task Update(Department item);
    }
}
