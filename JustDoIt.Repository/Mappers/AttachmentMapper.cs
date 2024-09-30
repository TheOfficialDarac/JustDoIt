using Riok.Mapperly.Abstractions;
using JustDoIt.Model.Database;
using JustDoIt.Model.Responses.Attachments;

namespace JustDoIt.Repository.Mappers
{
    [Mapper]
    public partial class AttachmentMapper
    {
        [MapProperty(nameof(Attachment.Id), nameof(AttachmentResponse.AttachmentId))]
        public partial AttachmentResponse ToResponse(Attachment dto);

        [MapProperty(nameof(Attachment.Id), nameof(AttachmentResponse.AttachmentId))]
        public partial List<AttachmentResponse> ToResponseList(IEnumerable<Attachment> dtos);

        //public partial Attachment CreateRequestToType(CreateAttachmentRequest dto);

        //public partial CreateAttachmentResponse TypeToCreateResponse(Attachment taskAttachment);

        //public partial GetProjectRoleResponse TypeToGetRoleResponse(ProjectRole projectRole);
    }
}
