namespace Repositories
{
    public interface IMarkRepository
    {
        Task<object> GetAll();
        Task AddNew(object item);
        Task Update(object item);
    }
}
