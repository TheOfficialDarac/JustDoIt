using JustDoIt.Model.Responses.Abstractions;

namespace JustDoIt.Model.Responses.Projects
{
    public class GetProjectRoleResponse : Response
    {
        public string RoleName { get; set; } = string.Empty;
        public string RoleDescription { get; set; } = string.Empty;
    }
}
