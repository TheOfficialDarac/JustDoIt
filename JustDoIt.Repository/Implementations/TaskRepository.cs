using JustDoIt.DAL;
using JustDoIt.Model.DTOs.Requests.Abstractions;
using JustDoIt.Model.DTOs.Requests.Attachments;
using JustDoIt.Model.DTOs.Requests.Tasks;
using JustDoIt.Model.DTOs.Responses;
using JustDoIt.Model.DTOs.Responses.Attachments;
using JustDoIt.Model.DTOs.Responses.Tasks;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Repository.Mappers;
using Microsoft.EntityFrameworkCore;

namespace JustDoIt.Repository.Implementations
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

        public async Task<CreateTaskResponse> Create(CreateTaskRequest request)
        {
            // basic exceptions already handled up to repository
            // here only database errors exist
            try
            {
                var task = _mapper.CreateRequestToType(request);

                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();


                return _mapper.TypeToCreateResponse(task);
            }
            catch { /*Logger? */ }
            return new CreateTaskResponse();
        }

        public async Task<IEnumerable<TaskResponse>> GetAll(
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

                return _mapper.ToResponseList(tasks);
            }
            catch
            {
                /*Logger */
                return [];
            }
        }

        public async Task<TaskResponse> GetSingle(GetSingleItemRequest request)
        {
            try
            {
                var result = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id);

                if (result == null)
                {
                    return new TaskResponse();
                }
                return _mapper.ToResponse(result);
            }
            catch { /*Logger */ }
            return new TaskResponse();
        }

        public async Task<TaskResponse> Update(UpdateTaskRequest request)
        {
            try
            {
                var existing = await _context.Tasks.FindAsync(request.Id);

                // entity is not found in db, nothing to update
                if (existing is null) return new TaskResponse();

                existing.Title = request.Title;
                existing.Summary = request.Summary;
                existing.Description = request.Description;
                existing.PictureUrl = request.PictureUrl;
                existing.Deadline = request.Deadline;
                existing.IsActive = request.IsActive;
                existing.State = request.State;


                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();
                return _mapper.ToResponse(existing);
            }
            catch { /*Logger */ }
            return new TaskResponse();
        }

        public async Task<TaskResponse> Delete(GetSingleItemRequest request)
        {
            try
            {
                var existing = await _context.Tasks.FindAsync(request.Id);

                // entity is not in db, nothing to delete
                if (existing is null) return new TaskResponse();

                _context.Tasks.Remove(existing);
                await _context.SaveChangesAsync();
                return new TaskResponse();
            }
            catch { /*Logger */ }
            return new TaskResponse { Id = request.Id };
        }

        public async Task<IEnumerable<TaskResponse>> GetUserTasks(GetSingleUserRequest request)
        {
            try
            {
                var query = _context.UserTasks
                    .Where(ut => ut.UserId.Equals(request.Id))
                    .Select(t => t.Task);

                var tasks = await query.ToListAsync();
                return _mapper.ToResponseList(tasks);
            }
            catch { /* Logger */}
            return [];
        }

        public async Task<IEnumerable<TaskResponse>> GetUserProjectTasks(GetUserProjectTasksRequest request)
        {
            try
            {

                var tasks = await _context.UserTasks
                    .Where(ut => ut.UserId.Equals(request.UserId))
                    .Select(t => t.Task)
                    .Where(t => t.ProjectId.Equals(request.ProjectId)).ToListAsync();

                //var tasks = await query.ToListAsync();
                return _mapper.ToResponseList(tasks);
            }
            catch { /* Logger */ }
            return [];
        }
        #endregion
    }
}
