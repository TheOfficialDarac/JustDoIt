using JustDoIt.DAL;
using JustDoIt.Model.Responses.Utils;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Repository.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JustDoIt.Repository.Implementations 
{
    public class UtilsRepository(ApplicationContext context) : IUtilsRepository
    {
        private readonly ApplicationContext _context = context;
        private readonly UtilsMapper _mapper = new UtilsMapper();

        public async Task<IEnumerable<CategoryResponse>> GetAllCategories()
        {
            try
            {
                var response = await _context.Categories.ToListAsync();
                return _mapper.ToCategoryResponseList(response);
            }
            catch (Exception) { /* Logger */ }
            return [];
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllProjectCategories(int projectId)
        {
            try
            {
                var response = await _context.ProjectCategories.Where(x => x.ProjectId == projectId).Select(x => x.Category).ToListAsync();

                return _mapper.ToCategoryResponseList(response);
            }
            catch (Exception) { /* Logger */ }
            return [];
        }

        public async Task<IEnumerable<StatusResponse>> GetAllStatuses()
        {
            try
            {
                var response = await _context.Statuses.ToListAsync();
                return _mapper.ToStatusResponseList(response);
            }
            catch (Exception) { /* Logger */ }
            return [];
        }

        public async Task<StatusResponse> GetProjectStatus(int projectId)
        {
            try
            {
                var project = await _context.Projects.FindAsync(projectId);
                if (project != null)
                {
                    var response = await _context.Statuses.FindAsync(project.StatusId);
                    if (response != null)
                    {
                        return _mapper.ToStatusResponse(response);
                    }
                }
            }
            catch (Exception) { /* Logger */ }
            return new();
        }
        public async Task<IEnumerable<StateResponse>> GetAllStates()
        {
            try
            {
                var response = await _context.States.ToListAsync();
                return _mapper.ToStateResponseList(response);
            }
            catch (Exception) { /* Logger */ }
            return [];
        }

        public async Task<StateResponse> GetTaskState(int taskId)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(taskId);
                if (task != null)
                {
                    var response = await _context.States.FindAsync(task.StateId);
                    if(response!= null)
                        return _mapper.ToStateResponse(response);
                }
            }
            catch (Exception) { /* Logger */ }
            return new();
        }
    }
}
