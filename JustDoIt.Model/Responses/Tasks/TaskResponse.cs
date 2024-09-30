using JustDoIt.Model.Responses.Abstractions;

namespace JustDoIt.Model.Responses.Tasks
{
    public class TaskResponse : Response
    {
        public int Id { get; set; } = 0;
        public string IssuerId { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ProjectId { get; set; } = 0;
        public string PictureUrl { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime Deadline { get; set; } = DateTime.UtcNow;
        public DateTime LastChangeDate { get; set; } = DateTime.UtcNow;
        public int PriorityId { get; set; } = 0;
        public int StateId { get; set; } = 0;
        public int StatusId { get; set; } = 0;
        public IEnumerable<int> Tags { get; set; } = [];
    }
}
