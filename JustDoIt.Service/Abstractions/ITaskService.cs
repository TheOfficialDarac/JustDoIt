using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Service.Definitions.Common;

namespace JustDoIt.Service.Definitions
{
    interface ITaskService : IGenericService<TaskDTO>
    {
        #region Tasks

        Task<Tuple<IEnumerable<TaskDTO>, Result>> GetUserTasks(
            string userID
        );
        Task<Tuple<IEnumerable<TaskDTO>, Result>> GetAll(string? title,
            string? description,
            string? pictureURL,
            DateTime? deadlineStart,
            DateTime? deadlineEnd,
            string? state,
            string? adminID,
            int? projectID);

        //Task<IEnumerable<Model.Task>> GetTasks(
        //    string? title,
        //    string? description,
        //    string? pictureURL,
        //    DateTime? deadlineStart,
        //    DateTime? deadlineEnd,
        //    string? state,
        //    string? adminID,
        //    int? projectID,
        //    int page = 1,
        //    int pageSize = 5
        //);
        //Task<Model.Task> GetTask(int id);
        //Task<bool> UpdateTask(Model.Task task);

        //Task<bool> CreateTask(TaskDTO taskDTO);
        //Task<bool> DeleteTask(int taskID);
        #endregion Tasks
    }
}
