using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests.Tasks;
using JustDoIt.Repository.Abstractions.Common;

namespace JustDoIt.Repository.Abstractions
{
    public interface ITaskRepository : IGenericRepository<TaskDTO>
    {
        Task<IEnumerable<TaskDTO>> GetUserTasks(string userID);
        Task<IEnumerable<TaskDTO>> GetAll(GetTasksRequest searchParams);

        Task<IEnumerable<TaskDTO>> GetUserProjectTasks(string userID, int projectID);
    }
}
