using JustDoIt.Common;
using JustDoIt.Model.DTOs.Requests.Abstractions;
using JustDoIt.Model.DTOs.Requests.Attachments;
using JustDoIt.Model.DTOs.Responses;
using JustDoIt.Model.DTOs.Responses.Attachments;
using JustDoIt.Model.DTOs.Responses.Projects;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Service.Abstractions.Common;
using JustDoIt.Service.Errors;

namespace JustDoIt.Service.Implementations
{
    public class AttachmentService(IAttachmentRepository repository) : IAttachmentService
    {
        #region Properties

        private readonly IAttachmentRepository _repository = repository;
        #endregion

        #region Methods

        public async Task<RequestResponse<CreateAttachmentResponse>> Create(CreateAttachmentRequest request)
        {
            var response = await _repository.Create(request);
            if (response.Id != 0)
            {
                return new RequestResponse<CreateAttachmentResponse>(response, Result.Success());
            }
            var errors = new List<Error> { ProjectErrors.NotFound };

            return new RequestResponse<CreateAttachmentResponse>(new CreateAttachmentResponse(), Result.Failure(errors));
        }

        public async Task<RequestResponse<AttachmentResponse>> Delete(GetSingleItemRequest request)
        {
            var errors = new List<Error>();
            var response = await _repository.Delete(request);
            if (response.Id == 0)
            {
                return new RequestResponse<AttachmentResponse>(response, Result.Success());
            }
            errors.Add(AttachmentErrors.NotFound);
            return new RequestResponse<AttachmentResponse>(response, Result.Failure(errors));
        }

        public async Task<RequestResponse<AttachmentResponse>> GetAll(GetAttachmentRequest request)
        {
            var errors = new List<Error>();

            var response = await _repository.GetAll(request);

            if (response.Any()) return new RequestResponse<AttachmentResponse>(response, Result.Success());

            errors.Add(AttachmentErrors.NotFound);
            //errors.Add(ProjectErrors.BadRequest);

            return new RequestResponse<AttachmentResponse>([], Result.Failure(errors));
        }

        public async Task<RequestResponse<AttachmentResponse>> GetSingle(GetSingleItemRequest request)
        {
            var errors = new List<Error>();
            var response = await _repository.GetSingle(request);

            if (response.Id == 0)
            {
                errors.Add(ProjectErrors.NotFound);
                errors.Add(ProjectErrors.BadRequest);
                return new RequestResponse<AttachmentResponse>(response, Result.Failure(errors));
            }

            return new RequestResponse<AttachmentResponse>(response, Result.Success());
        }

        public async Task<RequestResponse<AttachmentResponse>> Update(UpdateAttachmentRequest request)
        {
            var errors = new List<Error>();
            if (request.Id == 0)
            {
                errors.Add(ProjectErrors.BadRequest);
                return new RequestResponse<AttachmentResponse>(new AttachmentResponse(), Result.Failure(errors));
            }

            var response = await _repository.Update(request);
            if (response.Id == 0)
            {
                errors.Add(ProjectErrors.NotFound);
                return new RequestResponse<AttachmentResponse>(new AttachmentResponse(), Result.Failure(errors));
            }

            return new RequestResponse<AttachmentResponse>(new AttachmentResponse(), Result.Success());
        }

        public async Task<bool> UpdateTaskAttachments(UpdateTaskAttachmentsRequest request)
        {
            await _repository.UpdateTaskAttachments(request);
            return true;
        }

        #endregion
    }
}
