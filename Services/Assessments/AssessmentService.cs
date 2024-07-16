
using DataAccess.Models;
using Repositories;

namespace Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentRepository _repository;

        public AssessmentService() => _repository = new AssessmentRepository();

        public async Task AddNew(Assessment item) => await _repository.AddNew(item);

        public async Task<List<Assessment>> GetAll() => await _repository.GetAll();

        public async Task Update(Assessment item) => await _repository.Update(item);
    }
}
