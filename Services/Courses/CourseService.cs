
using DataAccess.Models;
using Repositories;

namespace Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;

        public CourseService() => _repository = new CourseRepository();

        public async Task AddNew(Course item) => await _repository.AddNew(item);

        public async Task<List<Course>> GetAll() => await _repository.GetAll();

        public async Task Update(Course item) => await _repository.Update(item);
    }
}
