using JustDoIt.Model.DTOs.Requests.Abstractions;
using JustDoIt.Model.DTOs.Requests.Projects;
using JustDoIt.Model.DTOs.Responses;
using JustDoIt.Model.DTOs.Responses.Projects;
using JustDoIt.Service.Abstractions.Common;

namespace JustDoIt.Service.Abstractions
{
    public interface IProjectService : IGenericService<ProjectResponse, CreateProjectRequest, CreateProjectResponse, GetProjectsRequest, GetSingleItemRequest, UpdateProjectRequest>
    {
        Task<RequestResponse<ProjectResponse>> GetUserProjects(GetSingleUserRequest request);
    }
}
