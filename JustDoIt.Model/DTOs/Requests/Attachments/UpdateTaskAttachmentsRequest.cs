using JustDoIt.Model.DTOs.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.DTOs.Requests.Attachments
{
    public class UpdateTaskAttachmentsRequest:UpdateRequest
    {
        public int TaskId { get; set; } = 0;
        public List<IFormFile>? Attachments { get; set; } = null;

    }
}
