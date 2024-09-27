using JustDoIt.Model.DTOs.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.DTOs.Requests.Tasks
{
    public class CreateTaskRequest : CreateRequest
    {
        public string? Title { get; set; }

        public string Summary { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int ProjectId { get; set; } = 0;

        public string IssuerId { get; set; } = string.Empty;

        public string PictureUrl { get; set; } = string.Empty;

        public string Deadline { get; set; } = string.Empty;
        public IFormFile? Attachment { get; set; } = null;

    }
}
