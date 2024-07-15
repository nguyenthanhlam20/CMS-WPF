using Repositories;

namespace Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService()
        {
            _repository = new DepartmentRepository();
        }

        public Task AddNew(object item) => _repository.AddNew(item);

        public Task<object> GetAll() => _repository.GetAll();

        public Task Update(object item) => _repository.Update(item);
    }
}
