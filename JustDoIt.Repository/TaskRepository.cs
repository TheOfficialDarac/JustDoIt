using Azure;
using JustDoIt.Common;
using JustDoIt.DAL;
using JustDoIt.Mapperly;
using JustDoIt.Model.DTOs;
using JustDoIt.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JustDoIt.Repository
{
    class TaskRepository : IGenericRepository<TaskDTO>
    {
        #region Properties

        private ApplicationContext _context { get; set; }
        #endregion

        public TaskRepository(ApplicationContext context)
        {
            _context = context;
        }

        #region Methods

        public async Task<bool> Create(TaskDTO entity)
        {
            // basic exceptions already handled up to repository
            // here only database errors exist
            try
            {
                var mapper = new TaskMapper();
                var task = mapper.MapToType(entity);
                
                var result = await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                //Logger?
                return false;
            }
        }

        public async Task<IEnumerable<TaskDTO>> GetAll(
            string? title,
            string? description,
            string? pictureURL,
            DateTime? deadlineStart,
            DateTime? deadlineEnd,
            string? state,
            string? adminID,
            int? projectID) {
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

                var tasks = await query.ToListAsync();
                var mapper = new TaskMapper();

                var results = mapper.MapToDTOList(tasks);

                return results;

            }
            catch
            {
                return null;
            }
        }

        public async Task<TaskDTO> GetSingle(int id)
        {
            try
            {
                var mapper = new TaskMapper();
                var result = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

                if (result == null)
                {
                    return null;
                }
                return mapper.MapToDTO(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Update(TaskDTO entity)
        {
            try
            {
                var existing = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == entity.Id);

                if (existing == null)
                {
                    return false;
                }

                existing.Title = entity.Title;
                existing.AdminId = entity.AdminId;
                existing.Description = entity.Description;
                existing.ProjectId = entity.ProjectId;
                existing.PictureUrl = entity.PictureUrl;
                existing.Deadline = entity.Deadline;
                existing.State = entity.State;


                _context.ChangeTracker.DetectChanges();
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(TaskDTO entity)
        {
            try
            {
                var existing = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == entity.Id);

                if (existing is null)
                {
                    return false;
                }

                _context.Tasks.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
