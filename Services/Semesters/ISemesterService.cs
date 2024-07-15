namespace Services
{
    public interface ISemesterService
    {
        Task<object> GetAll();
        Task AddNew(object item);
        Task Update(object item);
    }
}
