using System.Text;
using JustDoIt.DAL;
using JustDoIt.Model;
using JustDoIt.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;

namespace JustDoIt.Repository
{
    public class Repository : IRepository
    {
        #region Properties

        private readonly DataContext _context;
        #endregion Properties

        #region Constructor

        public Repository(DataContext context)
        {
            _context = context;
        }
        #endregion Constructor

        #region Methods

        #region Tasks

        public async Task<IEnumerable<Model.Task>> GetTasks(
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
        )
        {
            try
            {
                var query = _context.Tasks.AsQueryable();

                if (!string.IsNullOrEmpty(title))
                {
                    query = query.Where(t => t.Title.Contains(title));
                }

                if (!string.IsNullOrEmpty(description))
                {
                    query = query.Where(t => t.Description.Contains(description));
                }

                if (!string.IsNullOrEmpty(pictureURL))
                {
                    query = query.Where(t => t.PictureUrl == pictureURL);
                }

                if (!string.IsNullOrEmpty(state))
                {
                    query = query.Where(t => t.State == state);
                }

                if (deadlineStart.HasValue)
                {
                    deadlineStart = DateTime.SpecifyKind(deadlineStart.Value, DateTimeKind.Utc);
                    query = query.Where(t => t.Deadline >= deadlineStart);
                }

                if (deadlineEnd.HasValue)
                {
                    deadlineEnd = DateTime.SpecifyKind(deadlineEnd.Value, DateTimeKind.Utc);
                    query = query.Where(t => t.Deadline <= deadlineEnd);
                }

                if (!adminID.IsNullOrEmpty())
                {
                    query = query.Where(t => t.AdminId == adminID);
                }

                if (projectID.HasValue)
                {
                    query = query.Where(t => t.ProjectId == projectID);
                }

                var results = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Model.Task> GetTask(int id)
        {
            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
                return (task is null) ? null : task;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> PutTask(Model.Task task)
        {
            try
            {
                var existing = await _context.Tasks.FindAsync(task.Id);

                if (existing == null)
                {
                    return false;
                }

                existing.Title = task.Title;
                existing.AdminId = task.AdminId;
                existing.Description = task.Description;
                existing.ProjectId = task.ProjectId;
                existing.PictureUrl = task.PictureUrl;
                existing.Deadline = task.Deadline;
                existing.State = task.State;
                existing.Admin = task.Admin;
                existing.Project = task.Project;
                existing.Attachments = task.Attachments;
                existing.Comments = task.Comments;
                existing.Labels = task.Labels;
                existing.Users = task.Users;


                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteTask(int taskID)
        {
            try
            {
                var existing = await _context.Tasks.FindAsync(taskID);

                if (existing is null)
                {
                    return false;
                }

                _context.Tasks.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateTask(Model.Task task)
        {
            try
            {
                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion Tasks

        #region Projects

        public async Task<IEnumerable<Project>> GetProjects(
            string? title,
            string? description,
            string? pictureURL,
            string? adminID,
            int page = 1,
            int pageSize = 5
        )
        {
            try
            {
                var query = _context.Projects.AsQueryable();

                if (!string.IsNullOrEmpty(title))
                {
                    query = query.Where(p => p.Title.Contains(title));
                }

                if (!string.IsNullOrEmpty(description))
                {
                    query = query.Where(p => p.Description != null &&
                                             p.Description.Contains(description));
                }

                if (!string.IsNullOrEmpty(pictureURL))
                {
                    query = query.Where(p => p.PictureUrl != null && p.PictureUrl == pictureURL);
                }

                if (!adminID.IsNullOrEmpty())
                {
                    query = query.Where(t => t.AdminId == adminID);
                }

                var results = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Project> GetProject(int id)
        {
            try
            {
                var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
                return (project is null) ? null : project;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateProject(Project project)
        {
            try
            {
                var existing = await _context.Projects.FindAsync(project.Id);

                if (existing == null)
                {
                    return false;
                }

                existing.AdminId = project.AdminId;
                existing.Title = project.Title;
                existing.PictureUrl = project.PictureUrl;
                existing.Description = project.Description;
                // existing.Admin          = project.Admin;
                // existing.Attachments    = project.Attachments;
                // existing.Tasks          = project.Tasks;
                // existing.UserProjects   = project.UserProjects;

                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProject(int projectID)
        {
            try
            {
                var existing = await _context.Projects.FindAsync(projectID);

                if (existing is null)
                {
                    return false;
                }

                _context.Projects.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateProject(Project project)
        {
            try
            {
                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion Projects

        #region Users

        public async Task<IEnumerable<Model.Task>> GetTasksOfUser(string userID)
        {
            try
            {
                var results = await _context.Users
                .Where(u => u.Id == userID)
                .SelectMany(u => u.Tasks)
                .ToListAsync();
                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Project>> GetProjectsOfUser(string userID)
        {
            try
            {
                var results = await _context.Users
               .Where(u => u.Id == userID)
               .SelectMany(u => u.Projects)
               .ToListAsync();
                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<AppUser>> GetUsers(
            string? username,
            string? firstName,
            string? lastName,
            string? email,
            string? pictureURL,
            int page = 1,
            int pageSize = 5)
        {

            try
            {
                var query = _context.AppUsers.AsQueryable();

                if (!string.IsNullOrEmpty(username))
                {
                    query = query.Where(p => p.UserName.Contains(username));
                }

                if (!string.IsNullOrEmpty(firstName))
                {
                    query = query.Where(p =>
                    p.FirstName != null &&
                    p.FirstName.Contains(firstName));
                }

                if (!string.IsNullOrEmpty(lastName))
                {
                    query = query.Where(t =>
                    t.LastName != null &&
                    t.LastName.Contains(lastName));
                }

                if (!string.IsNullOrEmpty(email))
                {
                    query = query.Where(t => t.Email == email);
                }

                if (!string.IsNullOrEmpty(pictureURL))
                {
                    query = query.Where(t => t.PictureUrl == pictureURL);
                }

                var results = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AppUser> GetUser(string id)
        {
            try
            {
                var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == id);
                return (user is null) ? null : user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateUser(AppUser user)
        {
            // AspNetCore.Identity.UserManager.

            try
            {
                var existing = await _context.AppUsers.FindAsync(user.Id);

                if (existing == null)
                {
                    return false;
                }

                existing.UserName = user.UserName;
                // existing.Password = user.Password;
                existing.FirstName = user.FirstName;
                existing.LastName = user.LastName;
                existing.Email = user.Email;
                existing.PictureUrl = user.PictureUrl;

                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUser(string userID)
        {
            try
            {
                var existing = await _context.AppUsers.FindAsync(userID);

                if (existing is null)
                {
                    return false;
                }

                _context.AppUsers.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateUser(AppUser user)
        {
            try
            {
                await _context.AppUsers.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion Users

        #region Labels

        public async Task<IEnumerable<Label>> GetLabels(
            string? title,
            string? description,
            int? taskID,
            int page = 1,
            int pageSize = 5)
        {
            try
            {
                var query = _context.Labels.AsQueryable();

                if (!string.IsNullOrEmpty(title))
                {
                    query = query.Where(l => l.Title.Contains(title));
                }

                if (!string.IsNullOrEmpty(description))
                {
                    query = query.Where(l =>
                    l.Description != null &&
                    l.Description.Contains(description));
                }

                if (taskID.HasValue)
                {
                    query = query.Where(l => l.TaskId == taskID);
                }

                var results = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Label> GetLabel(int id)
        {
            try
            {
                var result = await _context.Labels.FirstOrDefaultAsync(l => l.Id == id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateLabel(Label label)
        {
            try
            {
                var existing = await _context.Labels.FindAsync(label.Id);

                if (existing == null)
                {
                    return false;
                }

                existing.Title = label.Title;
                existing.Description = label.Description;

                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLabel(int labelID)
        {
            try
            {
                var existing = await _context.Labels.FindAsync(labelID);

                if (existing is null)
                {
                    return false;
                }

                _context.Labels.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateLabel(Label label)
        {
            try
            {
                await _context.Labels.AddAsync(label);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion Labels

        #region Comments

        public async Task<IEnumerable<Comment>> GetComments(
            string? text,
            int? taskID,
            string? userID,
            int page = 1,
            int pageSize = 5)
        {
            try
            {
                var query = _context.Comments.AsQueryable();

                if (!string.IsNullOrEmpty(text))
                {
                    query = query.Where(l =>
                    l.Text != null &&
                    l.Text.Contains(text));
                }

                if (taskID.HasValue)
                {
                    query = query.Where(l => l.TaskId == taskID);
                }

                if (!userID.IsNullOrEmpty())
                {
                    query = query.Where(l => l.UserId == userID);
                }

                var results = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Comment> GetComment(int id)
        {
            try
            {
                var result = await _context.Comments.FirstOrDefaultAsync(l => l.Id == id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateComment(Comment comment)
        {
            try
            {
                var existing = await _context.Comments.FindAsync(comment.Id);

                if (existing == null)
                {
                    return false;
                }

                existing.Text = comment.Text;
                existing.UserId = comment.UserId;
                existing.TaskId = comment.TaskId;

                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteComment(int commentID)
        {
            try
            {
                var existing = await _context.Comments.FindAsync(commentID);

                if (existing is null)
                {
                    return false;
                }

                _context.Comments.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateComment(Comment comment)
        {
            try
            {
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion Comments

        #region Attachments

        public async Task<IEnumerable<Attachment>> GetAttachments(
            string? filepath,
            int? taskID,
            int? projectID,
            int page = 1,
            int pageSize = 5)
        {
            try
            {
                var query = _context.Attachments.AsQueryable();

                if (!string.IsNullOrEmpty(filepath))
                {
                    query = query.Where(l =>
                    l.Filepath.Contains(filepath));
                }

                if (taskID.HasValue)
                {
                    query = query.Where(l => l.TaskId == taskID);
                }

                if (projectID.HasValue)
                {
                    query = query.Where(l => l.ProjectId == projectID);
                }

                var results = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Attachment> GetAttachment(int id)
        {
            try
            {
                var result = await _context.Attachments.FirstOrDefaultAsync(a => a.Id == id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateAttachment(Attachment attachment)
        {
            try
            {
                var existing = await _context.Attachments.FindAsync(attachment.Id);

                if (existing == null)
                {
                    return false;
                }

                existing.Filepath = attachment.Filepath;
                existing.ProjectId = attachment.ProjectId;
                existing.TaskId = attachment.TaskId;

                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAttachment(int attachmentID)
        {
            try
            {
                var existing = await _context.Attachments.FindAsync(attachmentID);

                if (existing is null)
                {
                    return false;
                }

                _context.Attachments.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateAttachment(Attachment attachment)
        {
            try
            {
                await _context.Attachments.AddAsync(attachment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UserProject>> GetUserProjects(string? userId, int? projectID, bool? isVerified, string? token, string? role, int page = 1, int pageSize = 5)
        {
            try
            {
                var query = _context.UserProjects.AsQueryable();

                if (!string.IsNullOrEmpty(userId))
                {
                    query = query.Where(up =>
                    up.UserId.Contains(userId));
                }

                if (projectID.HasValue)
                {
                    query = query.Where(up => up.ProjectId == projectID);
                }

                if (isVerified.HasValue)
                {
                    query = query.Where(up => up.IsVerified == isVerified);
                }

                if (!string.IsNullOrEmpty(token))
                {
                    query = query.Where(up => up.Token != null &&
                    up.Token.Contains(token));
                }

                if (!string.IsNullOrEmpty(role))
                {
                    query = query.Where(up => up.ProjectRole != null &&
                    up.ProjectRole.Contains(role));
                }

                var results = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion Attachments

        #endregion Methods
    }
}