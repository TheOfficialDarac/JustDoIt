using System.Text;
using JustDoIt.DAL;
using JustDoIt.Model;
using JustDoIt.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
            int? adminID,
            int? projectID,
            int page = 1,
            int pageSize = 5
        ) {
            try {
                var query = _context.Tasks.AsQueryable();

                if(!string.IsNullOrEmpty(title)) {
                    query = query.Where(t => t.Title.Contains(title)); 
                }

                if(!string.IsNullOrEmpty(description)) {
                    query = query.Where(t => t.Description.Contains(description));
                }
            
                if(!string.IsNullOrEmpty(pictureURL)) {
                    query = query.Where(t => t.PictureUrl == pictureURL);
                }

                if(!string.IsNullOrEmpty(state)) {
                    query = query.Where(t => t.State == state);
                }

                if(deadlineStart.HasValue) {
                    deadlineStart = DateTime.SpecifyKind(deadlineStart.Value, DateTimeKind.Utc);
                    query = query.Where(t => t.Deadline >= deadlineStart);
                }

                if(deadlineEnd.HasValue) {
                    deadlineEnd = DateTime.SpecifyKind(deadlineEnd.Value, DateTimeKind.Utc);
                    query = query.Where(t => t.Deadline <= deadlineEnd);
                }

                if(adminID.HasValue) {
                    query = query.Where(t => t.AdminId == adminID);   
                }

                if(projectID.HasValue) {
                    query = query.Where(t => t.ProjectId == projectID);   
                }

                var results = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return results;
            } catch (Exception e){
                throw new Exception(e.Message);
            }
        }

        public async Task<Model.Task> GetTask(int id) {
            try {
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
                return (task is null) ? null : task;
            } catch (Exception e) {
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
                
                existing.Title          = task.Title;
                existing.AdminId        = task.AdminId;
                existing.Description    = task.Description;
                existing.ProjectId      = task.ProjectId;
                existing.PictureUrl     = task.PictureUrl;
                existing.Deadline       = task.Deadline;
                existing.State          = task.State;
                existing.Admin          = task.Admin;
                existing.Project        = task.Project;
                existing.Attachments    = task.Attachments;
                existing.Comments       = task.Comments;
                existing.Labels         = task.Labels;
                existing.Users          = task.Users;

                
                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteTask(Model.Task task)
        {
            try
            {
                var existing = await _context.Tasks.FindAsync(task.Id);

                if(existing is null) {
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
            int? adminID,
            int page = 1,
            int pageSize = 5
        ) {
            try {
                var query = _context.Projects.AsQueryable();

                if(!string.IsNullOrEmpty(title)) {
                    query = query.Where(p => p.Title.Contains(title)); 
                }

                if(!string.IsNullOrEmpty(description)) {
                    query = query.Where(p => p.Description != null && 
                                             p.Description.Contains(description));
                }
            
                if(!string.IsNullOrEmpty(pictureURL)) {
                    query = query.Where(t => t.PictureUrl == pictureURL);
                }

                if(adminID.HasValue) {
                    query = query.Where(t => t.AdminId == adminID);   
                }

                var results = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return results;
            } catch (Exception e){
                throw new Exception(e.Message);
            } 
        }

        public async Task<Project> GetProject(int id)
        {
            try {
                var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
                return (project is null) ? null : project;
            } catch (Exception e) {
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
                
                existing.AdminId        = project.AdminId;
                existing.Title          = project.Title;
                existing.PictureUrl     = project.PictureUrl;
                existing.Description    = project.Description;
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

        public async Task<bool> DeleteProject(Project project)
        {
            try
            {
                var existing = await _context.Projects.FindAsync(project.Id);

                if(existing is null) {
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

        public async Task<IEnumerable<AppUser>> GetUsers(
            string? username, 
            string? firstName, 
            string? lastName, 
            string? email, 
            string? pictureURL, 
            int page = 1, 
            int pageSize = 5) {

            try {
                var query = _context.AppUsers.AsQueryable();

                if(!string.IsNullOrEmpty(username)) {
                    query = query.Where(p => p.Username.Contains(username)); 
                }

                if(!string.IsNullOrEmpty(firstName)) {
                    query = query.Where(p => 
                    p.FirstName != null &&
                    p.FirstName.Contains(firstName));
                }
            
                if(!string.IsNullOrEmpty(lastName)) {
                    query = query.Where(t => 
                    t.LastName != null &&
                    t.LastName.Contains(lastName));
                }

                if(!string.IsNullOrEmpty(email)) {
                    query = query.Where(t => t.Email == email);
                }

                if(!string.IsNullOrEmpty(pictureURL)) {
                    query = query.Where(t => t.PictureUrl == pictureURL);
                }

                var results = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return results;
            } catch (Exception e) {
                throw new Exception(e.Message);
            } 
        }

        public async Task<AppUser> GetUser(int id)
        {
            try {
                var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == id);
                return (user is null) ? null : user;
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateUser(AppUser user)
        {
            try
            {
                var existing = await _context.AppUsers.FindAsync(user.Id);
                
                if (existing == null)
                {
                    return false;   
                }
                
                existing.Username = user.Username;
                existing.Password = user.Password;
                existing.FirstName = user.FirstName;
                existing.LastName = user.LastName;
                existing.Email = user.Email;
                existing.IsVerified = user.IsVerified;
                existing.Token = user.Token;
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

        public async Task<bool> DeleteUser(AppUser user)
        {
            try
            {
                var existing = await _context.AppUsers.FindAsync(user.Id);

                if(existing is null) {
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

        public async Task<IEnumerable<Label>> GetLabels(
            string? title, 
            string? description, 
            int? taskID,
            int page = 1, 
            int pageSize = 5) {
            try {
                var query = _context.Labels.AsQueryable();

                if(!string.IsNullOrEmpty(title)) {
                    query = query.Where(l => l.Title.Contains(title)); 
                }

                if(!string.IsNullOrEmpty(description)) {
                    query = query.Where(l => 
                    l.Description != null &&
                    l.Description.Contains(description));
                }

                if(taskID.HasValue) {
                    query = query.Where(l => l.TaskId == taskID);
                }
            
                var results = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                return results;
            } catch (Exception e) {
                throw new Exception(e.Message);
            } 
        }

        public async Task<Label> GetLabel(int id)
        {
            try {
                var result = await _context.Labels.FirstOrDefaultAsync(l => l.Id == id);
                return result;
            } catch (Exception e) {
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

        public async Task<bool> DeleteLabel(Label label)
        {
            try
            {
                var existing = await _context.Labels.FindAsync(label.Id);

                if(existing is null) {
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
        #endregion Users

        #endregion Methods
    }
}