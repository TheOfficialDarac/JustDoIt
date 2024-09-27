using JustDoIt.Model.Requests.Abstractions;

namespace JustDoIt.Model.Requests.Attachments
{
    public class GetAttachmentRequest : GetRequest
    {
        public int TaskId { get; set; } = 0;
        public int AttachmentId { get; set; } = 0;
    }
}
