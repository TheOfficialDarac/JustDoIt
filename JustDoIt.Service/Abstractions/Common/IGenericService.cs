using JustDoIt.Common;

namespace JustDoIt.Service.Definitions.Common
{
    interface IGenericService<T> where T : class
    {
        Task<Result> Create(T entity);
        Task<Tuple<IEnumerable<T>, Result>> GetAll();
        Task<Tuple<T, Result>> GetSingle(int id);
        Task<Result> Update(T entity);
        Task<Result> Delete(T entity);
    }
}
