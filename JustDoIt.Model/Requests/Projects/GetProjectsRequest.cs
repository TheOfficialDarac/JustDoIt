using JustDoIt.Model.Requests.Abstractions;

namespace JustDoIt.Model.Requests.Projects
{
    public class GetProjectsRequest : GetRequest
    {
        public string? Title { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? MinCreatedDate { get; set; }
        public DateTime? MaxCreatedDate { get; set; }
    }
}
