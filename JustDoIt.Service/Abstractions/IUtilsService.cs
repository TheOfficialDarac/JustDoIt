using JustDoIt.Model.Responses;
using JustDoIt.Model.Responses.Utils;

namespace JustDoIt.Service.Abstractions
{
    public interface IUtilsService
    {
        Task<RequestResponse<CategoryResponse>> GetAllCategories();
        Task<RequestResponse<CategoryResponse>> GetAllProjectCategories(int projectId);
        Task<RequestResponse<StateResponse>> GetAllStates();
        Task<RequestResponse<StatusResponse>> GetAllStatuses();
        Task<RequestResponse<StateResponse>> GetTaskState(int taskId);
        Task<RequestResponse<StatusResponse>> GetProjectStatus(int projectId);
    }
}
