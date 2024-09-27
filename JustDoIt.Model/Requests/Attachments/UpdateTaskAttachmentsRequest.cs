using JustDoIt.Model.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.Requests.Attachments
{
    public class UpdateTaskAttachmentsRequest : UpdateRequest
    {
        public int TaskId { get; set; } = 0;
        public List<IFormFile>? Attachments { get; set; } = null;

    }
}
