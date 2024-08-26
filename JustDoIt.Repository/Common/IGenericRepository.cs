using JustDoIt.Common;

namespace JustDoIt.Repository.Common
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> Create(T entity);
        Task<IEnumerable<T>?> GetAll(string? title,
            string? description,
            string? pictureURL,
            DateTime? deadlineStart,
            DateTime? deadlineEnd,
            string? state,
            string? adminID,
            int? projectID); // could create a query arguments class
        Task<T?> GetSingle(int id);
        Task<T?> Update(T entity);
        Task<T?> Delete(T entity);
    }
}
