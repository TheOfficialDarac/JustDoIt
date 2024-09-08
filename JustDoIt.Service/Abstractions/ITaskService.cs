using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Service.Definitions.Common;

namespace JustDoIt.Service.Definitions
{
    public interface ITaskService : IGenericService<TaskDTO>
    {
        #region Tasks

        Task<Tuple<IEnumerable<TaskDTO>?, Result>> GetUserTasks(string userID);
        Task<Tuple<IEnumerable<TaskDTO>?, Result>> GetAll(TaskSearchParams searchParams);
        Task<Tuple<IEnumerable<TaskDTO>?, Result>> GetUserProjectTasks(string userID, int projectID);
        #endregion Tasks
    }
}
