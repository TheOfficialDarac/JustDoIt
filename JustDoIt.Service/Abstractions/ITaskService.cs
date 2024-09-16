using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests.Tasks;
using JustDoIt.Service.Abstractions.Common;

namespace JustDoIt.Service.Abstractions
{
    public interface ITaskService : IGenericService<TaskDTO>
    {
        Task<(IEnumerable<TaskDTO> data, Result result)> GetUserTasks(string userID);
        Task<(IEnumerable<TaskDTO> data, Result result)> GetAll(GetTasksRequest searchParams);
        Task<(IEnumerable<TaskDTO> data, Result result)> GetUserProjectTasks(string userID, int projectID);
    }
}
