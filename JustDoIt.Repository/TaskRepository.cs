using Azure;
using JustDoIt.Common;
using JustDoIt.DAL;
using JustDoIt.Mapperly;
using JustDoIt.Model.DTOs;
using JustDoIt.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                var mapper = new TaskMapper();
                var task = mapper.MapToType(entity);
                
                var result = await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
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
            catch (Exception)
            {
                return null;
            }
            throw new NotImplementedException();
        }

        public Task<TaskDTO> GetSingle(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TaskDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(TaskDTO entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
