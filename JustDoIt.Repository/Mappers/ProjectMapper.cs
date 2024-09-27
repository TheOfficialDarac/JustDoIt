using JustDoIt.Model.Database;
using JustDoIt.Model.Requests.Projects;
using JustDoIt.Model.Responses.Projects;
using Riok.Mapperly.Abstractions;

namespace JustDoIt.Repository.Mappers
{
    [Mapper]
    public partial class ProjectMapper
    {

        public partial ProjectResponse ToResponse(Project dto);

        public partial List<ProjectResponse> ToResponseList(List<Project> dtos);

        public partial Project CreateRequestToType(CreateProjectRequest dto);

        public partial CreateProjectResponse TypeToCreateResponse(Project task);

        public partial GetProjectRoleResponse TypeToGetRoleResponse(ProjectRole projectRole);
    }
}
