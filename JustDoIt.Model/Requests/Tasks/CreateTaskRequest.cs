using JustDoIt.Model.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.Requests.Tasks
{
    public class CreateTaskRequest : CreateRequest
    {
        public string IssuerId { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ProjectId { get; set; } = 0;
        public string PictureUrl { get; set; } = string.Empty;
        public string Deadline { get; set; } = string.Empty;
        public IFormFile? Picture { get; set; } = null;
        public int PriorityId { get; set; } = 0;
        public int StateId { get; set; } = 0;
        public int StatusId { get; set; } = 0;
        public IEnumerable<int> Tags { get; set; } = [];
    }
}
