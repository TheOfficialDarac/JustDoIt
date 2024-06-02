using JustDoIt.Model;

namespace JustDoIt.Repository.Common
{
    public interface IRepository
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

        Task<bool> PutTask(Model.Task task);

        Task<bool> DeleteTask(Model.Task task);

        Task<bool> CreateTask(Model.Task task);
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

         Task<Project> GetProject(int id);

        Task<bool> UpdateProject(Project task);

        Task<bool> DeleteProject(Project task);

        Task<bool> CreateProject(Project task);
        #endregion Projects

        #region Users
        Task<IEnumerable<AppUser>> GetUsers(
            string? username,
            string? firstName,
            string? lastName,
            string? email,
            string? pictureURL,
            int page = 1,
            int pageSize = 5
        );

        Task<AppUser> GetUser(int id);

        Task<bool> UpdateUser(AppUser user);

        Task<bool> DeleteUser(AppUser user);

        Task<bool> CreateUser(AppUser user);
        #endregion Users

        #region Labels
        Task<IEnumerable<Label>> GetLabels(
            string? title,
            string? description,
            int? taskID,
            int page = 1,
            int pageSize = 5
        );

        Task<Label> GetLabel(int id);

        Task<bool> UpdateLabel(Label label);

        Task<bool> DeleteLabel(Label label);

        Task<bool> CreateLabel(Label label); 
        #endregion Labels
    }
}
