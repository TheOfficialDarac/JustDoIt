using JustDoIt.Model;
using Riok.Mapperly.Abstractions;
using JustDoIt.Model.DTOs.Responses.Attachments;
using JustDoIt.Model.DTOs.Requests.Attachments;

namespace JustDoIt.Repository.Mappers
{
    [Mapper]
    public partial class AttachmentMapper
    {
        public partial AttachmentResponse ToResponse(TaskAttachment dto);
        public partial List<AttachmentResponse> ToResponseList(List<TaskAttachment> dtos);

        public partial TaskAttachment CreateRequestToType(CreateAttachmentRequest dto);

        public partial CreateAttachmentResponse TypeToCreateResponse(TaskAttachment taskAttachment);


        //public partial GetProjectRoleResponse TypeToGetRoleResponse(ProjectRole projectRole);
    }
}
