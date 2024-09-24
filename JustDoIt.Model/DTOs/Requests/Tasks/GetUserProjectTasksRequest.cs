using JustDoIt.Model.DTOs.Requests.Abstractions;

namespace JustDoIt.Model.DTOs.Requests.Tasks
{
    public class GetUserProjectTasksRequest : GetRequest
    {
        public string? UserId { get; set; } = string.Empty;
        public int ProjectId { get; set; } = 0;
    }
}
