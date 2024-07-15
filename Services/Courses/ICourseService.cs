namespace Services
{
    public interface ICourseService
    {
        Task<object> GetAll();
        Task AddNew(object item);
        Task Update(object item);
    }
}
