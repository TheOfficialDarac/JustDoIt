namespace JustDoIt.API.Contracts.Requests.Tasks
{
    public class UpdateTaskRequest
    {
        public string? Title { get; set; }

        public string? Summary { get; set; }

        public string? Description { get; set; }

        public string? PictureUrl { get; set; }

        public DateTime? Deadline { get; set; }

        public bool IsActive { get; set; }

        public string? State { get; set; }
    }
}
