using JustDoIt.Model.DTOs.Requests.Abstractions;
using JustDoIt.Model.DTOs.Requests.Attachments;
using JustDoIt.Model.DTOs.Responses.Attachments;
using JustDoIt.Repository.Abstractions.Common;

namespace JustDoIt.Repository.Abstractions
{
    public interface IAttachmentRepository:IGenericRepository<AttachmentResponse, CreateAttachmentRequest, CreateAttachmentResponse, GetAttachmentRequest, GetSingleItemRequest, UpdateAttachmentRequest>
    {
    }
}
