using JustDoIt.Model.Requests.Abstractions;

namespace JustDoIt.Model.Requests.Projects
{
    public class GetProjectsRequest : GetRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public int StatusId { get; set; } = 0;
        public DateTime? MinCreatedDate { get; set; }
        public DateTime? MaxCreatedDate { get; set; }

        public IEnumerable<int> Category = [];
    }
}
