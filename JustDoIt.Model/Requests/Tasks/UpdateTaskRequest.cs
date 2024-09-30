using JustDoIt.Model.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.Requests.Tasks
{
    public class UpdateTaskRequest : UpdateRequest
    {
        public int Id { get; init; } = 0;
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; } = DateTime.MinValue;
        public IFormFile? Picture { get; set; } = null;
        public int PriorityId { get; set; } = 0;
        public int StateId { get; set; } = 0;
        public int StatusId { get; set; } = 0;
        public IEnumerable<int> Tags { get; set; } = [];
    }
}
