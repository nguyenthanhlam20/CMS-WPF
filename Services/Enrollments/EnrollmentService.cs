
using DataAccess.Models;
using Repositories;

namespace Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repository;

        public EnrollmentService() => _repository = new EnrollmentRepository();

        public async Task AddNew(Enrollment item) => await _repository.AddNew(item);

        public async Task<List<Enrollment>> GetAll() => await _repository.GetAll();

        public async Task Update(Enrollment item) => await _repository.Update(item);
    }
}
