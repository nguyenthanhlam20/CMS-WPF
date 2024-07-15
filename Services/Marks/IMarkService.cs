namespace Services
{
    public interface IMarkService
    {
        Task<object> GetAll();
        Task AddNew(object item);
        Task Update(object item);
    }
}
