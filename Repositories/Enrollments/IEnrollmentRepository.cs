namespace Repositories
{
    public interface IEnrollmentRepository
    {
        Task<object> GetAll();
        Task AddNew(object item);
        Task Update(object item);
    }
}
