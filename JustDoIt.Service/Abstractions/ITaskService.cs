using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Service.Abstractions.Common;

namespace JustDoIt.Service.Abstractions
{
    public interface ITaskService : IGenericService<TaskDTO>
    {
        Task<Tuple<IEnumerable<TaskDTO>, Result>> GetUserTasks(string userID);
        Task<Tuple<IEnumerable<TaskDTO>, Result>> GetAll(TaskSearchParams searchParams);
        Task<Tuple<IEnumerable<TaskDTO>, Result>> GetUserProjectTasks(string userID, int projectID);
    }
}
