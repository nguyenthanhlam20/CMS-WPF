
using DataAccess.Models;
using Repositories;

namespace Services
{
    public class SemesterService : ISemesterService
    {
        private readonly ISemesterRepository _repository;

        public SemesterService() => _repository = new SemesterRepository();

        public async Task AddNew(Semester item) => await _repository.AddNew(item);

        public async Task<List<Semester>> GetAll() => await _repository.GetAll();

        public async Task Update(Semester item) => await _repository.Update(item);
    }
}
