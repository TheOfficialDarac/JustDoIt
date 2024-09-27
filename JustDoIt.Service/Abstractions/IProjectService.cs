using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Projects;
using JustDoIt.Model.Responses;
using JustDoIt.Model.Responses.Projects;
using JustDoIt.Service.Abstractions.Common;

namespace JustDoIt.Service.Abstractions
{
    public interface IProjectService : IGenericService<ProjectResponse, CreateProjectRequest, CreateProjectResponse, GetProjectsRequest, GetSingleItemRequest, UpdateProjectRequest>
    {
        Task<RequestResponse<ProjectResponse>> GetUserProjects(GetSingleUserRequest request);
        Task<RequestResponse<GetProjectRoleResponse>> GetUserRoleInProjectAsync(GetProjectRoleRequest request);
    }
}
