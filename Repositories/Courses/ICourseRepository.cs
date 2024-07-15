namespace Repositories
{
    public interface ICourseRepository
    {
        Task<object> GetAll();
        Task AddNew(object item);
        Task Update(object item);
    }
}
