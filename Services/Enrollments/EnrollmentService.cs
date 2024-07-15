using Repositories;

namespace Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repository;

        public EnrollmentService()
        {
            _repository = new EnrollmentRepository();
        }

        public Task AddNew(object item) => _repository.AddNew(item);

        public Task<object> GetAll() => _repository.GetAll();

        public Task Update(object item) => _repository.Update(item);
    }
}
