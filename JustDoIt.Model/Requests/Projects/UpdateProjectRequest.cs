using JustDoIt.Model.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.Requests.Projects
{
    public class UpdateProjectRequest : UpdateRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string PictureUrl { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public IFormFile? Attachment { get; set; } = null;

    }
}
