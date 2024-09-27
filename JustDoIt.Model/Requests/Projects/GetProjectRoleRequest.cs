namespace JustDoIt.Model.Requests.Projects
{
    public class GetProjectRoleRequest
    {
        public string UserId { get; set; } = string.Empty;
        public int ProjectId { get; set; } = 0;
    }
}
