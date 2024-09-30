using JustDoIt.Model.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.Requests.Attachments
{
    public class UpdateAttachmentsRequest:UpdateRequest
    {
        public int TypeId { get; set; } = 0;
        public IEnumerable<IFormFile> Attachments { get; set; } = [];
    }
}
