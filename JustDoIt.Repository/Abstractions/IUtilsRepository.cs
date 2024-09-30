using JustDoIt.Model.Responses.Utils;

namespace JustDoIt.Repository.Abstractions
{
    public interface IUtilsRepository
    {
        Task<IEnumerable<CategoryResponse>> GetAllCategories();
        Task<IEnumerable<CategoryResponse>> GetAllProjectCategories(int projectId);
        Task<IEnumerable<StatusResponse>> GetAllStatuses();
        Task<StatusResponse> GetProjectStatus(int projectId);
    }
}
