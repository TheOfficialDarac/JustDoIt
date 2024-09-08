using Azure;
using JustDoIt.Common;
using JustDoIt.DAL;
using JustDoIt.Mapperly;
using JustDoIt.Model;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JustDoIt.Repository
{
    public class TaskRepository : ITaskRepository
    {
        #region Properties

        private ApplicationContext _context { get; set; }
        private TaskMapper _mapper { get; set; }
        #endregion

        public TaskRepository(ApplicationContext context)
        {
            _context = context;
            _mapper = new TaskMapper();
        }

        #region Methods

        public async Task<TaskDTO?> Create(TaskDTO entity)
        {
            // basic exceptions already handled up to repository
            // here only database errors exist
            try
            {
                var task = _mapper.MapToType(entity);

                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();

                await _context.UserTasks.AddAsync(
                    new UserTask {
                        TaskId = task.Id,
                        UserId = task.IssuerId,
                        AssignDate = DateTime.Now,
                        IsActive = true
                    });
                await _context.SaveChangesAsync();

                return _mapper.MapToDTO(task);
            }
            catch { /*Logger? */ }
            return null;
        }
        public async Task<IEnumerable<TaskDTO>?> GetAll()
        {
            var query = _context.Tasks.AsQueryable();
            var result = await query.ToListAsync();
            return _mapper.MapToDTOList(result);
        }

        public async Task<IEnumerable<TaskDTO>?> GetAll(
        TaskSearchParams searchParams)
        {
            try
            {
                var query = _context.Tasks.AsQueryable();

                if (!string.IsNullOrEmpty(searchParams.Title))
                {
                    query = query.Where(t => !string.IsNullOrEmpty(t.Title) && t.Title.Contains(searchParams.Title));
                }

                if (!string.IsNullOrEmpty(searchParams.IssuerId))
                {
                    query = query.Where(t => t.IssuerId.Contains(searchParams.IssuerId));
                }

                if (!string.IsNullOrEmpty(searchParams.Summary))
                {
                    query = query.Where(t => !string.IsNullOrEmpty(t.Summary) && t.Summary.Contains(searchParams.Summary));
                }

                if (!string.IsNullOrEmpty(searchParams.Description))
                {
                    query = query.Where(t => !string.IsNullOrEmpty(t.Description) && t.Description.Contains(searchParams.Description));
                }

                if (searchParams.ProjectId.HasValue)
                {
                    query = query.Where(t => t.ProjectId == searchParams.ProjectId);
                }

                // search by picture?
                //if (!string.IsNullOrEmpty(searchParams.Description))
                //{
                //    query = query.Where(t => t.Description.Contains(searchParams.Description));
                //}

                if (searchParams.DeadlineStart.HasValue)
                {
                    searchParams.DeadlineStart = DateTime.SpecifyKind(searchParams.DeadlineStart.Value, DateTimeKind.Utc);
                    query = query.Where(t => t.Deadline >= searchParams.DeadlineStart);
                }

                if (searchParams.DeadlineEnd.HasValue)
                {
                    searchParams.DeadlineEnd = DateTime.SpecifyKind(searchParams.DeadlineEnd.Value, DateTimeKind.Utc);
                    query = query.Where(t => t.Deadline <= searchParams.DeadlineEnd);
                }

                if (searchParams.MinCreatedDate.HasValue)
                {
                    searchParams.MinCreatedDate = DateTime.SpecifyKind(searchParams.MinCreatedDate.Value, DateTimeKind.Utc);
                    query = query.Where(t => t.CreatedDate >= searchParams.MinCreatedDate);
                }

                if (searchParams.MaxCreatedDate.HasValue)
                {
                    searchParams.MaxCreatedDate = DateTime.SpecifyKind(searchParams.MaxCreatedDate.Value, DateTimeKind.Utc);
                    query = query.Where(t => t.CreatedDate <= searchParams.MaxCreatedDate);
                }


                if (searchParams.IsActive.HasValue)
                {
                    query = query.Where(t => t.IsActive == searchParams.IsActive);
                }

                if (!string.IsNullOrEmpty(searchParams.State))
                {
                    query = query.Where(t => !string.IsNullOrEmpty(t.State) && t.State.Contains(searchParams.State));
                }

                var tasks = await query.ToListAsync();
                var results = _mapper.MapToDTOList(tasks);

                return results;
            }
            catch
            {
                /*Logger */
                return null;
            }
        }

        public async Task<TaskDTO?> GetSingle(int id)
        {
            try
            {
                var result = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

                if (result == null)
                {
                    return null;
                }
                return _mapper.MapToDTO(result);
            }
            catch
            {
                /*Logger */
                return null;
            }
        }

        public async Task<TaskDTO?> Update(TaskDTO entity)
        {
            try
            {
                var existing = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == entity.Id);

                // entity is not found in db, nothing to update
                if (existing is null) return null;

                existing.Title = entity.Title;
                existing.Summary = entity.Summary;
                existing.Description = entity.Description;
                existing.ProjectId = entity.ProjectId;
                existing.PictureUrl = entity.PictureUrl;
                existing.Deadline = entity.Deadline;
                existing.IsActive = entity.IsActive;
                existing.State = entity.State;


                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();
                return entity;
            }
            catch
            { /*Logger */
                return null;
            }
        }

        public async Task<TaskDTO?> Delete(TaskDTO entity)
        {
            try
            {
                var existing = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == entity.Id);

                // entity is not in db, nothing to delete
                if (existing is null) return entity;

                _context.Tasks.Remove(existing);
                await _context.SaveChangesAsync();
                return null;
            }
            catch
            { /*Logger */
                return entity;
            }
        }

        public async Task<IEnumerable<TaskDTO>?> GetUserTasks(string userID)
        {
            try
            {
                var query = _context.UserTasks
                    .Where(ut => ut.UserId.Equals(userID))
                    .Select(t => t.Task);

                var tasks = await query.ToListAsync();
                return _mapper.MapToDTOList(tasks);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<TaskDTO>?> GetUserProjectTasks(string userID, int projectID)
        {
            try
            {
                //  get project find all tasks
                //  get user tasks
                //  get common inputs

                var query = _context.UserTasks
                    .Where(ut => ut.UserId.Equals(userID))
                    .Select(t => t.Task)
                    .Where(t => t.ProjectId.Equals(projectID));

                var tasks = await query.ToListAsync();
                return _mapper.MapToDTOList(tasks);
            }
            catch
            {
                /* Logger */
                return null;
            }
        }

        #endregion
    }
}
