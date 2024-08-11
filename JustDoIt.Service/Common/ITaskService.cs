using JustDoIt.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Service.Common
{
    interface ITaskService
    {
        #region Tasks

        Task<IEnumerable<Model.Task>> GetTasksOfUser(
            string userID
        );
        Task<IEnumerable<Model.Task>> GetTasks(
            string? title,
            string? description,
            string? pictureURL,
            DateTime? deadlineStart,
            DateTime? deadlineEnd,
            string? state,
            string? adminID,
            int? projectID,
            int page = 1,
            int pageSize = 5
        );
        Task<Model.Task> GetTask(int id);
        Task<bool> UpdateTask(Model.Task task);

        Task<bool> CreateTask(TaskDTO taskDTO);
        Task<bool> DeleteTask(int taskID);
        #endregion Tasks
    }
}
