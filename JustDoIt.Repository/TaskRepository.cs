using Azure;
using JustDoIt.Common;
using JustDoIt.DAL;
using JustDoIt.Mapperly;
using JustDoIt.Model;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests.Tasks;
using JustDoIt.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JustDoIt.Repository
{
    public class TaskRepository : ITaskRepository
    {
        #region Properties

        private readonly ApplicationContext _context;
        private readonly TaskMapper _mapper;
        #endregion

        public TaskRepository(ApplicationContext context)
        {
            _context = context;
            _mapper = new TaskMapper();
        }

        #region Methods

        public async Task<TaskDTO> Create(TaskDTO entity)
        {
            // basic exceptions already handled up to repository
            // here only database errors exist
            try
            {
                var task = _mapper.MapToType(entity);

                await _context.Tasks.AddAsync(task);

                //await _context.UserTasks.AddAsync(
                //    new UserTask {
                //        TaskId = task.Id,
                //        UserId = task.IssuerId,
                //        AssignDate = DateTime.Now,
                //        IsActive = true
                //    });
                await _context.SaveChangesAsync();


                return _mapper.MapToDTO(task);
            }
            catch { /*Logger? */ }
            return new TaskDTO();
        }
        public async Task<IEnumerable<TaskDTO>> GetAll()
        {
            var query = _context.Tasks.AsQueryable();
            var result = await query.ToListAsync();
            return _mapper.MapToDTOList(result);
        }

        public async Task<IEnumerable<TaskDTO>> GetAll(
        GetTasksRequest request)
        {
            try
            {
                var query = _context.Tasks.AsQueryable();

                if (!string.IsNullOrEmpty(request.Title))
                {
                    query = query.Where(t => !string.IsNullOrEmpty(t.Title) && t.Title.Contains(request.Title));
                }

                if (!string.IsNullOrEmpty(request.IssuerId))
                {
                    query = query.Where(t => t.IssuerId.Contains(request.IssuerId));
                }

                if (!string.IsNullOrEmpty(request.Summary))
                {
                    query = query.Where(t => !string.IsNullOrEmpty(t.Summary) && t.Summary.Contains(request.Summary));
                }

                //if (!string.IsNullOrEmpty(request.Description))
                //{
                //    query = query.Where(t => !string.IsNullOrEmpty(t.Description) && t.Description.Contains(request.Description));
                //}

                if (request.ProjectId.HasValue)
                {
                    query = query.Where(t => t.ProjectId == request.ProjectId);
                }

                // search by picture?
                //if (!string.IsNullOrEmpty(request.Description))
                //{
                //    query = query.Where(t => t.Description.Contains(request.Description));
                //}

                if (request.DeadlineStart.HasValue)
                {
                    request.DeadlineStart = DateTime.SpecifyKind(request.DeadlineStart.Value, DateTimeKind.Utc);
                    query = query.Where(t => t.Deadline >= request.DeadlineStart);
                }

                if (request.DeadlineEnd.HasValue)
                {
                    request.DeadlineEnd = DateTime.SpecifyKind(request.DeadlineEnd.Value, DateTimeKind.Utc);
                    query = query.Where(t => t.Deadline <= request.DeadlineEnd);
                }

                if (request.MinCreatedDate.HasValue)
                {
                    request.MinCreatedDate = DateTime.SpecifyKind(request.MinCreatedDate.Value, DateTimeKind.Utc);
                    query = query.Where(t => t.CreatedDate >= request.MinCreatedDate);
                }

                if (request.MaxCreatedDate.HasValue)
                {
                    request.MaxCreatedDate = DateTime.SpecifyKind(request.MaxCreatedDate.Value, DateTimeKind.Utc);
                    query = query.Where(t => t.CreatedDate <= request.MaxCreatedDate);
                }


                if (request.IsActive.HasValue)
                {
                    query = query.Where(t => t.IsActive == request.IsActive);
                }

                if (!string.IsNullOrEmpty(request.State))
                {
                    query = query.Where(t => !string.IsNullOrEmpty(t.State) && t.State.Contains(request.State));
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

        public async Task<TaskDTO> GetSingle(int id)
        {
            try
            {
                var result = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

                if (result == null)
                {
                    return new TaskDTO();
                }
                return _mapper.MapToDTO(result);
            }
            catch
            {
                /*Logger */
                return new TaskDTO();
            }
        }

        public async Task<bool> Update(TaskDTO entity)
        {
            try
            {
                var existing = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == entity.Id);

                // entity is not found in db, nothing to update
                if (existing is null) return false;

                existing.Title = entity.Title;
                existing.Summary = entity.Summary;
                existing.Description = entity.Description;
                existing.PictureUrl = entity.PictureUrl;
                existing.Deadline = entity.Deadline;
                existing.IsActive = entity.IsActive!.Value;
                existing.State = entity.State;


                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            { /*Logger */
                return false;
            }
        }

        public async Task<bool> Delete(TaskDTO entity)
        {
            try
            {
                var existing = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == entity.Id);

                // entity is not in db, nothing to delete
                if (existing is null) return false;

                _context.Tasks.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            { /*Logger */
                return false;
            }
        }

        public async Task<IEnumerable<TaskDTO>> GetUserTasks(string userID)
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
                return new List<TaskDTO>();
            }
        }

        public async Task<IEnumerable<TaskDTO>> GetUserProjectTasks(string userID, int projectID)
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
                return new List<TaskDTO>();
            }
        }

        #endregion
    }
}
