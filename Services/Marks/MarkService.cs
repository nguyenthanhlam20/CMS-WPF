using Repositories;

namespace Services
{
    public class MarkService : IMarkService
    {
        private readonly IMarkRepository _repository;

        public MarkService()
        {
            _repository = new MarkRepository();
        }

        public Task AddNew(object item) => _repository.AddNew(item);

        public Task<object> GetAll() => _repository.GetAll();

        public Task Update(object item) => _repository.Update(item);
    }
}
