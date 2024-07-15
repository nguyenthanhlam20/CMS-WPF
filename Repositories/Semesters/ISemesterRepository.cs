namespace Repositories
{
    public interface ISemesterRepository
    {
        Task<object> GetAll();
        Task AddNew(object item);
        Task Update(object item);
    }
}
