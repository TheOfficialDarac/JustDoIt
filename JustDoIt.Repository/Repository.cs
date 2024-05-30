using JustDoIt.DAL;
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
                return task;
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        #endregion Methods
    }
}
