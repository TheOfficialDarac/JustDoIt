using JustDoIt.Model.Requests.Abstractions;

namespace JustDoIt.Model.Requests.Attachments
{
    public class GetAttachmentRequest:GetRequest
    {
        public int TypeId { get; set; } = 0;
        public int AttachmentId { get; set; } = 0;
    }
}
