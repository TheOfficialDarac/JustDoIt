using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Attachments;
using JustDoIt.Model.Responses;
using JustDoIt.Model.Responses.Attachments;

namespace JustDoIt.Service.Abstractions.Common
{
    public interface IAttachmentService : IGenericService<AttachmentResponse, CreateAttachmentRequest, CreateAttachmentResponse, GetAttachmentRequest, GetSingleItemRequest, UpdateAttachmentsRequest>
    {
        Task<RequestResponse<CreateAttachmentResponse>> CreateIssueAttachment(CreateAttachmentRequest request);
        Task<RequestResponse<CreateAttachmentResponse>> CreateTaskAttachment(CreateAttachmentRequest request);
        Task<RequestResponse<AttachmentResponse>> DeleteIssueAttachment(GetSingleItemRequest request);
        Task<RequestResponse<AttachmentResponse>> DeleteTaskAttachment(GetSingleItemRequest request);
        Task<RequestResponse<AttachmentResponse>> GetAllIssueAttachments(GetSingleItemRequest request);
        Task<RequestResponse<AttachmentResponse>> GetAllTaskAttachments(GetSingleItemRequest request);
        Task<RequestResponse<AttachmentResponse>> UpdateIssueAttachments(UpdateAttachmentsRequest request);
        Task<RequestResponse<AttachmentResponse>> UpdateTaskAttachments(UpdateAttachmentsRequest request);
    }
}
