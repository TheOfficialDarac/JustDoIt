﻿using JustDoIt.Common;

namespace JustDoIt.Repository.Abstractions.Common
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Create(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetSingle(int id);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
