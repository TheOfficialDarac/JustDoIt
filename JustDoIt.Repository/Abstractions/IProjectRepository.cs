using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Projects;
using JustDoIt.Model.Responses.Projects;
using JustDoIt.Repository.Abstractions.Common;

namespace JustDoIt.Repository.Abstractions
{
    public interface IProjectRepository : IGenericRepository<ProjectResponse, CreateProjectRequest, CreateProjectResponse, GetProjectsRequest, GetSingleItemRequest, UpdateProjectRequest>
    {
        Task<IEnumerable<ProjectResponse>> GetUserProjects(GetSingleUserRequest request);
        Task<GetProjectRoleResponse> GetUserRoleInProjectAsync(GetProjectRoleRequest request);
    }
}
