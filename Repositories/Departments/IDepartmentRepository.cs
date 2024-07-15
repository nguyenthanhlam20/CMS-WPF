namespace Repositories
{
    public interface IDepartmentRepository
    {
        Task<object> GetAll();
        Task AddNew(object item);
        Task Update(object item);
    }
}
