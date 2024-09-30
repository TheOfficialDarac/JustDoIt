using JustDoIt.Common;
using JustDoIt.Model.Responses;
using JustDoIt.Model.Responses.Utils;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Service.Abstractions;

namespace JustDoIt.Service.Implementations
{
    public class UtilsService(IUtilsRepository repository) : IUtilsService
    {
        private readonly IUtilsRepository _repository = repository;

        public async Task<RequestResponse<CategoryResponse>> GetAllCategories()
        {
            var response = await _repository.GetAllCategories();

            return new RequestResponse<CategoryResponse>(response, Result.Success());
        }
        public async Task<RequestResponse<CategoryResponse>> GetAllProjectCategories(int projectId)
        {
            var response = await _repository.GetAllProjectCategories(projectId);

            return new RequestResponse<CategoryResponse>(response, Result.Success());
        }

        public async Task<RequestResponse<StatusResponse>> GetAllStatuses()
        {
            var response = await _repository.GetAllStatuses();

            return new RequestResponse<StatusResponse>(response, Result.Success());
        }
        public async Task<RequestResponse<StatusResponse>> GetProjectStatus(int projectId)
        {
            var response = await _repository.GetProjectStatus(projectId);

            return new RequestResponse<StatusResponse>(response, Result.Success());
        }
    }
}
