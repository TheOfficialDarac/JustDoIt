using JustDoIt.Common;

namespace JustDoIt.Service.Abstractions.Common
{
    public interface IGenericService<T> where T : class
    {
        Task<(T data, Result result)> Create(T entity);
        Task<(IEnumerable<T> data, Result result)> GetAll();
        Task<(T data, Result result)> GetSingle(int id);
        Task<Result> Update(T entity);
        Task<Result> Delete(T entity);
    }
}
