using JustDoIt.Model.DTOs.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.DTOs.Requests.Tasks
{
    public class UpdateTaskRequest : UpdateRequest
    {
        public int Id { get; set; } = 0;

        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string PictureUrl { get; set; } = string.Empty;

        public DateTime Deadline { get; set; } = DateTime.Now.AddDays(7);

        public bool IsActive { get; set; } = true;

        public string State { get; set; }  = string.Empty;
        public IFormFile? Attachment { get; set; } = null;

    }
}
