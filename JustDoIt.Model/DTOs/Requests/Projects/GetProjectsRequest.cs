
using JustDoIt.Model.DTOs.Requests.Abstractions;

namespace JustDoIt.Model.DTOs.Requests.Projects
{
    public class GetProjectsRequest : GetRequest
    {
        public string? Title { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? MinCreatedDate { get; set; }
        public DateTime? MaxCreatedDate { get; set; }
    }
}
