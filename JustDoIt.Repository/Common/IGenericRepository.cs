using JustDoIt.Common;

namespace JustDoIt.Repository.Common
{
    interface IGenericRepository<T> where T : class
    {
        Task<bool> Create(T entity);
        Task<IEnumerable<T>> GetAll(string? title,
            string? description,
            string? pictureURL,
            DateTime? deadlineStart,
            DateTime? deadlineEnd,
            string? state,
            string? adminID,
            int? projectID);
        Task<T> GetSingle(int id);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
