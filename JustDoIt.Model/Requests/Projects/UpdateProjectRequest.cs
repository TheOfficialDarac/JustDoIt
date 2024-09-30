using JustDoIt.Model.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.Requests.Projects
{
    public class UpdateProjectRequest : UpdateRequest
    {
        public int Id { get; init; }
        public string Title { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int StatusId { get; set; } = 1;
        public IEnumerable<int> Category = [];
        //public string PictureUrl { get; set; } = string.Empty;
        public IFormFile? Attachment { get; set; } = null;
    }
}
