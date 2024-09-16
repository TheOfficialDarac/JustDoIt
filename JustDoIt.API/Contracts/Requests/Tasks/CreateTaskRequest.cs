namespace JustDoIt.API.Contracts.Requests.Tasks
{
    public class CreateTaskRequest
    {
        public string? Title { get; set; }

        public string? Summary { get; set; }

        public string? Description { get; set; }

        public int ProjectId { get; set; }

        public string? PictureUrl { get; set; }

        public DateTime? Deadline { get; set; }
    }
}
