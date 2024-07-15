namespace Repositories
{
    public interface IAssessmentRepository
    {
        Task<object> GetAll();
        Task AddNew(object item);
        Task Update(object item);
    }
}
