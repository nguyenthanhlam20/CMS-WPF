
using DataAccess.Models;
using Repositories;

namespace Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IStudentRepository _repository;

        public DepartmentService() => _repository = new StudentRepository();

        public async Task AddNew(Department item) => await _repository.AddNew(item);

        public async Task<List<Department>> GetAll() => await _repository.GetAll();

        public async Task Update(Department item) => await _repository.Update(item);
    }
}
