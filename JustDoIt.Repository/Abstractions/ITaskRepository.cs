using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Repository.Abstractions.Common;

namespace JustDoIt.Repository.Abstractions
{
    public interface ITaskRepository : IGenericRepository<TaskDTO>
    {
        Task<IEnumerable<TaskDTO>> GetUserTasks(string userID);
        Task<IEnumerable<TaskDTO>> GetAll(TaskSearchParams searchParams);

        Task<IEnumerable<TaskDTO>> GetUserProjectTasks(string userID, int projectID);
    }
}
