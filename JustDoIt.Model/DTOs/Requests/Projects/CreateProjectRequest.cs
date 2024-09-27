using JustDoIt.Model.DTOs.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.DTOs.Requests.Projects
{
    public class CreateProjectRequest : CreateRequest
    {
        public string IssuerId = string.Empty;
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string PictureUrl { get; set; } = string.Empty;
        public IFormFile? Attachment { get; set; } = null;

    }
}
