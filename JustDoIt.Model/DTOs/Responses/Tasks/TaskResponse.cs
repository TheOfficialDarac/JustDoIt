using JustDoIt.Model.DTOs.Responses.Abstractions;

namespace JustDoIt.Model.DTOs.Responses.Tasks
{
    public class TaskResponse: Response
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? IssuerId { get; set; }

        public string? Summary { get; set; }

        public string? Description { get; set; }

        public int? ProjectId { get; set; }

        public string? PictureUrl { get; set; }

        public DateTime? Deadline { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsActive { get; set; }

        public string? State { get; set; }
    }
}
