using JustDoIt.Model.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.Requests.Attachments
{
    public class UpdateAttachmentRequest : UpdateRequest
    {
        public int TaskId { get; set; } = 0;
        public int Id { get; set; } = 0;
        public string FilePath { get; set; } = string.Empty;
        public IFormFile? Attachment { get; set; } = null;

    }
}
