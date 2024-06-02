using JustDoIt.DAL;
using JustDoIt.Model;
using JustDoIt.Repository.Common;
using Microsoft.EntityFrameworkCore;

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
                    query = query.Where(p => p.Description.Contains(description));
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
                existing.Admin          = project.Admin;
                existing.Attachments    = project.Attachments;
                existing.Tasks          = project.Tasks;
                existing.UserProjects   = project.UserProjects;
                
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

        #endregion Methods
    }
}