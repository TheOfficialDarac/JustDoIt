using JustDoIt.DAL;
using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Tasks;
using JustDoIt.Model.Responses.Tasks;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Repository.Mappers;
using Microsoft.EntityFrameworkCore;

namespace JustDoIt.Repository.Implementations
{
    public class TaskRepository(ApplicationContext context) : ITaskRepository
    {
        #region Properties

        private readonly ApplicationContext _context = context;
        private readonly TaskMapper _mapper = new TaskMapper();

        #endregion

        #region Methods

        public async Task<CreateTaskResponse> Create(CreateTaskRequest request)
        {
            try
            {
                //request.Deadline = DateTime.SpecifyKind(new DateTime(request.Deadline), DateTimeKind.Utc);
                var task = _mapper.CreateRequestToType(request);

                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();


                return _mapper.TypeToCreateResponse(task);
            }
            catch (Exception e) { /*Logger? */ }
            return new CreateTaskResponse();
        }

        public async Task<IEnumerable<TaskResponse>> GetAll(
        GetTasksRequest request)
        {
            try
            {
                var query = _context.Tasks.AsQueryable();

                if (!string.IsNullOrEmpty(request.IssuerId))
                {
                    query = query.Where(t => t.IssuerId.Contains(request.IssuerId));
                }

                if (request.ProjectId != 0)
                {
                    query = query.Where(t => t.ProjectId == request.ProjectId);
                }

                if (request.PriorityId != 0)
                {
                    query = query.Where(t => t.PriorityId == request.PriorityId);
                }

                if (request.StateId != 0)
                {
                    query = query.Where(t => t.StateId == request.StateId);
                }

                if (request.StatusId != 0)
                {
                    query = query.Where(t => t.StatusId == request.StatusId);
                }

                if (!string.IsNullOrEmpty(request.Summary))
                {
                    query = query.Where(t => !string.IsNullOrEmpty(t.Summary) && t.Summary.Contains(request.Summary));
                }

                // search by description?

                //if (!string.IsNullOrEmpty(request.Description))
                //{
                //    query = query.Where(t => !string.IsNullOrEmpty(t.Description) && t.Description.Contains(request.Description));
                //}

                // search by picture?
                //if (!string.IsNullOrEmpty(request.Description))
                //{
                //    query = query.Where(t => t.Description.Contains(request.Description));
                //}

                if (request.DeadlineStart != DateTime.MinValue)
                {
                    request.DeadlineStart = DateTime.SpecifyKind(request.DeadlineStart, DateTimeKind.Utc);
                    query = query.Where(t => t.Deadline >= request.DeadlineStart);
                }

                if (request.DeadlineEnd != DateTime.MaxValue)
                {
                    request.DeadlineEnd = DateTime.SpecifyKind(request.DeadlineEnd, DateTimeKind.Utc);
                    query = query.Where(t => t.Deadline <= request.DeadlineEnd);
                }

                if (request.MinCreatedDate != DateTime.MinValue)
                {
                    request.MinCreatedDate = DateTime.SpecifyKind(request.MinCreatedDate, DateTimeKind.Utc);
                    query = query.Where(t => t.CreatedDate >= request.MinCreatedDate);
                }

                if (request.MaxCreatedDate != DateTime.MaxValue)
                {
                    request.MaxCreatedDate = DateTime.SpecifyKind(request.MaxCreatedDate, DateTimeKind.Utc);
                    query = query.Where(t => t.CreatedDate <= request.MaxCreatedDate);
                }

                if (request.Tags.Any())
                {
                    //retrieve all categories
                    var taskTags = await _context.TaskTags.Where(x => request.Tags.Contains(x.Id)).ToListAsync();
                    query = query.Where(x => taskTags.All(item => x.TaskTags.Contains(item)));
                }

                var tasks = await query.ToListAsync();

                return _mapper.ToResponseList(tasks);
            }
            catch (Exception)
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
            catch (Exception) { /*Logger */ }
            return new TaskResponse();
        }

        public async Task<TaskResponse> Update(UpdateTaskRequest request)
        {
            try
            {
                var existing = await _context.Tasks.FindAsync(request.Id);

                // entity is not found in db, nothing to update
                if (existing is null) return new TaskResponse();

                existing.Summary = request.Summary;
                existing.Description = request.Description;
                //existing.PictureUrl = request.PictureUrl;
                existing.Deadline = request.Deadline;
                existing.PriorityId = request.PriorityId;
                existing.StateId = request.StateId;
                existing.StatusId = request.StatusId;

                if (request.Tags.Any())
                {
                    //retrieve all categories
                    var taskTags = await _context.TaskTags.Where(x => request.Tags.Contains(x.Id)).ToListAsync();
                    existing.TaskTags = taskTags;
                }

                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();
                return _mapper.ToResponse(existing);
            }
            catch (Exception) { /*Logger */ }
            return new TaskResponse();
        }

        public async Task<TaskResponse> Delete(GetSingleItemRequest request)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(request.Id);

                // entity is not in db, nothing to delete
                if (task is null) return new TaskResponse();

                var userTasks = await _context.UserTasks.Where(x => x.Id == task.Id).ToListAsync();
                _context.UserTasks.RemoveRange(userTasks);

                var taskComments = await _context.TaskComments.Where(x => x.Id == task.Id).ToListAsync();
                _context.TaskComments.RemoveRange(taskComments);

                var taskAttachments = await _context.TaskAttachments.Where(x => x.Id == task.Id).ToListAsync();

                taskAttachments.ForEach(x => {
                    if (!string.IsNullOrEmpty(x.Attachment.Filepath))
                        File.Delete(x.Attachment.Filepath);

                    _context.Attachments.Remove(x.Attachment);
                });
                _context.TaskAttachments.RemoveRange(taskAttachments);

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                return new TaskResponse();
            }
            catch (Exception) { /* Logger */ }
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
            catch (Exception) { /* Logger */}
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
            catch (Exception) { /* Logger */ }
            return [];
        }
        #endregion
    }
}
