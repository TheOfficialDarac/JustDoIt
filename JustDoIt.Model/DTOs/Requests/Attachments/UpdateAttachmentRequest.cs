using JustDoIt.Model.DTOs.Requests.Abstractions;

namespace JustDoIt.Model.DTOs.Requests.Attachments
{
    public class UpdateAttachmentRequest:UpdateRequest
    {
        public int TaskId { get; set; } = 0;
        public int Id { get; set; } = 0;
        public string FilePath { get; set; } = string.Empty;
    }
}
