
using DataAccess.Models;
using Repositories;

namespace Services
{
    public class MarkService : IMarkService
    {
        private readonly IMarkRepository _repository;

        public MarkService() => _repository = new MarkRepository();

        public async Task AddNew(Mark item) => await _repository.AddNew(item);

        public async Task<List<Mark>> GetAll() => await _repository.GetAll();

        public async Task Update(Mark item) => await _repository.Update(item);
    }
}
