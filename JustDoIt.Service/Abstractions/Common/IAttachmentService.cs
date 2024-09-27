using JustDoIt.Model.DTOs.Requests.Abstractions;
using JustDoIt.Model.DTOs.Requests.Attachments;
using JustDoIt.Model.DTOs.Responses.Attachments;

namespace JustDoIt.Service.Abstractions.Common
{
    public interface IAttachmentService : IGenericService<AttachmentResponse, CreateAttachmentRequest, CreateAttachmentResponse, GetAttachmentRequest, GetSingleItemRequest, UpdateAttachmentRequest>
    {
        Task<bool> UpdateTaskAttachments(UpdateTaskAttachmentsRequest request);
    }
}
