namespace Services
{
    public interface IDepartmentService
    {
        Task<object> GetAll();
        Task AddNew(object item);
        Task Update(object item);
    }
}
