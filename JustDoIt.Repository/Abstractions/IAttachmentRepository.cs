using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Attachments;
using JustDoIt.Model.Responses.Attachments;
using JustDoIt.Repository.Abstractions.Common;

namespace JustDoIt.Repository.Abstractions
{
    public interface IAttachmentRepository : IGenericRepository<AttachmentResponse, CreateAttachmentRequest, CreateAttachmentResponse, GetAttachmentRequest, GetSingleItemRequest, UpdateAttachmentsRequest>
    {
        Task<CreateAttachmentResponse> CreateIssueAttachment(CreateAttachmentRequest request);
        Task<CreateAttachmentResponse> CreateTaskAttachment(CreateAttachmentRequest request);
        Task<AttachmentResponse> DeleteIssueAttachment(GetSingleItemRequest request);
        Task<AttachmentResponse> DeleteTaskAttachment(GetSingleItemRequest request);
        Task<IEnumerable<AttachmentResponse>> GetAllIssueAttachments(GetSingleItemRequest request);
        Task<IEnumerable<AttachmentResponse>> GetAllTaskAttachments(GetSingleItemRequest request);
        Task<AttachmentResponse> UpdateIssueAttachments(UpdateAttachmentsRequest request);
        Task<AttachmentResponse> UpdateTaskAttachments(UpdateAttachmentsRequest request);
    }
}
