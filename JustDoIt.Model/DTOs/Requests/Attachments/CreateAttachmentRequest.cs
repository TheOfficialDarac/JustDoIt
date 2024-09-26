using JustDoIt.Model.DTOs.Requests.Abstractions;

namespace JustDoIt.Model.DTOs.Requests.Attachments
{
    public class CreateAttachmentRequest : CreateRequest
    {
        public int TaskId { get; set; } = 0;
    }
}
