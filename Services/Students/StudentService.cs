
using DataAccess.Models;
using Repositories;

namespace Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService() => _repository = new StudentRepository();

        public async Task AddNew(Student item) => await _repository.AddNew(item);

        public async Task<List<Student>> GetAll() => await _repository.GetAll();

        public async Task Update(Student item) => await _repository.Update(item);
    }
}
