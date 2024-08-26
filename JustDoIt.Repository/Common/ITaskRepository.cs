using JustDoIt.Common;
using JustDoIt.Model.DTOs;

namespace JustDoIt.Repository.Common
{
    public interface ITaskRepository : IGenericRepository<TaskDTO>
    {
        Task<IEnumerable<TaskDTO>?> GetUserTasks(
           string userID
       );
        Task<IEnumerable<TaskDTO>?> GetAll(string? title,
            string? description,
            string? pictureURL,
            DateTime? deadlineStart,
            DateTime? deadlineEnd,
            string? state,
            string? adminID,
            int? projectID);
    }
}
