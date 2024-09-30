using JustDoIt.Model.Responses;
using JustDoIt.Model.Responses.Utils;

namespace JustDoIt.Service.Abstractions
{
    public interface IUtilsService
    {
        Task<RequestResponse<CategoryResponse>> GetAllCategories();
        Task<RequestResponse<CategoryResponse>> GetAllProjectCategories(int projectId);
        Task<RequestResponse<StatusResponse>> GetAllStatuses();
        Task<RequestResponse<StatusResponse>> GetProjectStatus(int projectId);
    }
}
