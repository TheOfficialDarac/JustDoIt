using JustDoIt.Model.DTOs.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.DTOs.Requests.Attachments
{
    public class CreateAttachmentRequest : CreateRequest
    {
        public int TaskId { get; set; } = 0;
        public IFormFile? Attachment { get; set; } = null;
    }
}
