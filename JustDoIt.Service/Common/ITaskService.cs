using JustDoIt.Common;
using JustDoIt.Model.DTOs;

namespace JustDoIt.Service.Common
{
    interface ITaskService
    {
        #region Tasks

        Tuple<Task<IEnumerable<Model.Task>>, IEnumerable<ErrorMessage>> GetUserTasks(
            string userID
        );
        Task<IEnumerable<TaskDTO>> GetAll(string? title,
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
