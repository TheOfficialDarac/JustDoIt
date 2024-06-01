using JustDoIt.Model;

namespace JustDoIt.Service.Common
{
    public interface IService
    {
        #region Tasks
        Task<IEnumerable<Model.Task>> GetTasks(
            string? title,
            string? description,
            string? pictureURL,
            DateTime? deadlineStart,
            DateTime? deadlineEnd,
            string? state,
            int? adminID,
            int? projectID,
            int page = 1,
            int pageSize = 5
        );
        Task<Model.Task> GetTask(int id);
        Task<bool> UpdateTask(Model.Task task);

        Task<bool> CreateTask(Model.Task task);
        Task<bool> DeleteTask(Model.Task task);
        #endregion Tasks

        #region Projects

        Task<IEnumerable<Project>> GetProjects(
            string? title,
            string? description,
            string? pictureURL,
            int? adminID,
            int page = 1,
            int pageSize = 5
        );
        #endregion Projects
    }
}
