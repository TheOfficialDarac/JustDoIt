using JustDoIt.Model.Requests.Abstractions;

namespace JustDoIt.Model.Requests.Tasks
{
    public class GetTasksRequest : GetRequest
    {
        public string IssuerId { get; set; } = string.Empty;
        public int ProjectId { get; set; } = 0;
        public string Summary { get; set; } = string.Empty;
        public DateTime DeadlineStart { get; set; } = DateTime.MinValue;
        public DateTime DeadlineEnd { get; set; } = DateTime.MaxValue;
        public DateTime MinCreatedDate { get; set; } = DateTime.MinValue;
        public DateTime MaxCreatedDate { get; set; } = DateTime.MaxValue;
        public int PriorityId { get; set; } = 0;
        public int StateId { get; set; } = 0;
        public int StatusId { get; set; } = 0;
        public IEnumerable<int> Tags { get; set; } = [];
    }
}
