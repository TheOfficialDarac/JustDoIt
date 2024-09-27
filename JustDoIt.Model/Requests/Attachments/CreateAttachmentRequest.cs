using JustDoIt.Model.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.Requests.Attachments
{
    public class CreateAttachmentRequest : CreateRequest
    {
        public int TaskId { get; set; } = 0;
        public IFormFile? Attachment { get; set; } = null;
    }
}
