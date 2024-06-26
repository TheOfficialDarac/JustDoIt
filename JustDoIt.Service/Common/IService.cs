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
            string? adminID,
            int? projectID,
            int page = 1,
            int pageSize = 5
        );
        Task<Model.Task> GetTask(int id);
        Task<bool> UpdateTask(Model.Task task);

        Task<bool> CreateTask(Model.Task task);
        Task<bool> DeleteTask(int taskID);
        #endregion Tasks

        #region Projects

        Task<IEnumerable<Project>> GetProjects(
            string? title,
            string? description,
            string? pictureURL,
            string? adminID,
            int page = 1,
            int pageSize = 5
        );

        Task<Project> GetProject(int id);

        Task<bool> UpdateProject(Project project);

        Task<bool> DeleteProject(int projectID);

        Task<bool> CreateProject(Project project);
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

        Task<AppUser> GetUser(string id);

        Task<bool> UpdateUser(AppUser user);

        Task<bool> DeleteUser(string userID);

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

        Task<bool> DeleteLabel(int labelID);

        Task<bool> CreateLabel(Label label);
        #endregion Labels

        #region Comments
        public Task<IEnumerable<Comment>> GetComments(
            string? text,
            int? taskID,
            string? userID,
            int page = 1,
            int pageSize = 5
        );
        Task<Comment> GetComment(int id);

        Task<bool> UpdateComment(Comment comment);

        Task<bool> DeleteComment(int commentID);

        Task<bool> CreateComment(Comment comment);
        #endregion Comments

        #region Attachments
        public Task<IEnumerable<Attachment>> GetAttachments(
            string? filepath,
            int? taskID,
            int? projectID,
            int page = 1,
            int pageSize = 5
        );
        Task<Attachment> GetAttachment(int id);

        Task<bool> UpdateAttachment(Attachment attachment);

        Task<bool> DeleteAttachment(int attachmentID);

        Task<bool> CreateAttachment(Attachment attachment);
        #endregion Attachments
    }
}
