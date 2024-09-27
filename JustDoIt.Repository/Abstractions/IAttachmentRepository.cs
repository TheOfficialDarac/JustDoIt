using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Attachments;
using JustDoIt.Model.Responses.Attachments;
using JustDoIt.Repository.Abstractions.Common;

namespace JustDoIt.Repository.Abstractions
{
    public interface IAttachmentRepository : IGenericRepository<AttachmentResponse, CreateAttachmentRequest, CreateAttachmentResponse, GetAttachmentRequest, GetSingleItemRequest, UpdateAttachmentRequest>
    {
        Task<bool> UpdateTaskAttachments(UpdateTaskAttachmentsRequest request);
    }
}
