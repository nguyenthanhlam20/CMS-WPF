namespace Services
{
    public interface IAssessmentService
    {
        Task<object> GetAll();
        Task AddNew(object item);
        Task Update(object item);
    }
}
