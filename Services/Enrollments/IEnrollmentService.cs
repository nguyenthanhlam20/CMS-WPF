﻿namespace Services
{
    public interface IEnrollmentService
    {
        Task<object> GetAll();
        Task AddNew(object item);
        Task Update(object item);
    }
}
