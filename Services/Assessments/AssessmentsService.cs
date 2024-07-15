﻿using Repositories;

namespace Services
{
    public class AssessmentsService : IAssessmentService
    {
        private readonly IAssessmentRepository _repository;

        public AssessmentsService()
        {
            _repository = new AssessmentsRepository();
        }

        public Task AddNew(object item) => _repository.AddNew(item);

        public Task<object> GetAll() => _repository.GetAll();

        public Task Update(object item) => _repository.Update(item);
    }
}
