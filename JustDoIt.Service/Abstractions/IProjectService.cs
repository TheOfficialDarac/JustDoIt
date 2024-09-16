using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests.Projects;
using JustDoIt.Service.Abstractions.Common;

namespace JustDoIt.Service.Abstractions
{
    public interface IProjectService: IGenericService<ProjectDTO>
    {
        Task<(ProjectDTO data, Result result)> Create(ProjectDTO entity, string userID);
        Task<(IEnumerable<ProjectDTO> data, Result result)> GetUserProjects(string userID);
        Task<(IEnumerable<ProjectDTO> data, Result result)> GetAll(GetProjectsRequest searchParams);
    }
}
