using JustDoIt.Common;

namespace JustDoIt.Repository.Common
{
    interface IGenericRepository<T> where T : class
    {
        Task<bool> Create(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetSingle(string id);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
