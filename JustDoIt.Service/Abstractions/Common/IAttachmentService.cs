using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Attachments;
using JustDoIt.Model.Responses.Attachments;

namespace JustDoIt.Service.Abstractions.Common
{
    public interface IAttachmentService : IGenericService<AttachmentResponse, CreateAttachmentRequest, CreateAttachmentResponse, GetAttachmentRequest, GetSingleItemRequest, UpdateAttachmentRequest>
    {
        Task<bool> UpdateTaskAttachments(UpdateTaskAttachmentsRequest request);
    }
}
