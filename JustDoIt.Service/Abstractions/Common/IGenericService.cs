using JustDoIt.Common;

namespace JustDoIt.Service.Definitions.Common
{
    public interface IGenericService<T> where T : class
    {
        abstract Task<Tuple<T, Result>> Create(T entity);
        Task<Tuple<IEnumerable<T>?, Result>> GetAll();
        Task<Tuple<T, Result>> GetSingle(int id);
        Task<Tuple<T, Result>> Update(T entity);
        Task<Tuple<T, Result>> Delete(T entity);
    }
}
