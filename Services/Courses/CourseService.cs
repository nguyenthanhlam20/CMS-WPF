using Repositories;

namespace Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;

        public CourseService()
        {
            _repository = new CourseRepository();
        }

        public Task AddNew(object item) => _repository.AddNew(item);

        public Task<object> GetAll() => _repository.GetAll();

        public Task Update(object item) => _repository.Update(item);
    }
}
