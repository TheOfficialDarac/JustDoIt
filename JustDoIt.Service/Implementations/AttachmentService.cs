using JustDoIt.Common;
using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Attachments;
using JustDoIt.Model.Responses;
using JustDoIt.Model.Responses.Attachments;
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

        #region Create
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

        public async Task<RequestResponse<CreateAttachmentResponse>> CreateTaskAttachment(CreateAttachmentRequest request)
        {
            var response = await _repository.CreateTaskAttachment(request);
            if (response.Id != 0)
            {
                return new RequestResponse<CreateAttachmentResponse>(response, Result.Success());
            }
            var errors = new List<Error> { ProjectErrors.NotFound };

            return new RequestResponse<CreateAttachmentResponse>(new CreateAttachmentResponse(), Result.Failure(errors));
        }

        public async Task<RequestResponse<CreateAttachmentResponse>> CreateIssueAttachment(CreateAttachmentRequest request)
        {
            var response = await _repository.CreateIssueAttachment(request);
            if (response.Id != 0)
            {
                return new RequestResponse<CreateAttachmentResponse>(response, Result.Success());
            }
            var errors = new List<Error> { ProjectErrors.NotFound };

            return new RequestResponse<CreateAttachmentResponse>(new CreateAttachmentResponse(), Result.Failure(errors));
        }
        #endregion


        #region Delete
        public async Task<RequestResponse<AttachmentResponse>> Delete(GetSingleItemRequest request)
        {
            var errors = new List<Error>();
            var response = await _repository.Delete(request);
            if (response.AttachmentId == 0)
            {
                return new RequestResponse<AttachmentResponse>(response, Result.Success());
            }
            errors.Add(AttachmentErrors.NotFound);
            return new RequestResponse<AttachmentResponse>(response, Result.Failure(errors));
        }

        public async Task<RequestResponse<AttachmentResponse>> DeleteTaskAttachment(GetSingleItemRequest request)
        {
            var errors = new List<Error>();
            var response = await _repository.DeleteTaskAttachment(request);
            if (response.AttachmentId == 0)
            {
                return new RequestResponse<AttachmentResponse>(response, Result.Success());
            }
            errors.Add(AttachmentErrors.NotFound);
            return new RequestResponse<AttachmentResponse>(response, Result.Failure(errors));
        }

        public async Task<RequestResponse<AttachmentResponse>> DeleteIssueAttachment(GetSingleItemRequest request)
        {
            var errors = new List<Error>();
            var response = await _repository.DeleteIssueAttachment(request);
            if (response.AttachmentId == 0)
            {
                return new RequestResponse<AttachmentResponse>(response, Result.Success());
            }
            errors.Add(AttachmentErrors.NotFound);
            return new RequestResponse<AttachmentResponse>(response, Result.Failure(errors));
        }
        #endregion

        #region GetAll
        public async Task<RequestResponse<AttachmentResponse>> GetAll(GetAttachmentRequest request)
        {
            var errors = new List<Error>();

            var response = await _repository.GetAll(request);

            if (response.Any()) return new RequestResponse<AttachmentResponse>(response, Result.Success());

            errors.Add(AttachmentErrors.NotFound);
            //errors.Add(ProjectErrors.BadRequest);

            return new RequestResponse<AttachmentResponse>([], Result.Failure(errors));
        }

        public async Task<RequestResponse<AttachmentResponse>> GetAllTaskAttachments(GetSingleItemRequest request)
        {
            var errors = new List<Error>();

            var response = await _repository.GetAllTaskAttachments(request);

            if (response.Any()) return new RequestResponse<AttachmentResponse>(response, Result.Success());

            errors.Add(AttachmentErrors.NotFound);
            //errors.Add(ProjectErrors.BadRequest);

            return new RequestResponse<AttachmentResponse>([], Result.Failure(errors));
        }

        public async Task<RequestResponse<AttachmentResponse>> GetAllIssueAttachments(GetSingleItemRequest request)
        {
            var errors = new List<Error>();

            var response = await _repository.GetAllIssueAttachments(request);

            if (response.Any()) return new RequestResponse<AttachmentResponse>(response, Result.Success());

            errors.Add(AttachmentErrors.NotFound);
            //errors.Add(ProjectErrors.BadRequest);

            return new RequestResponse<AttachmentResponse>([], Result.Failure(errors));
        }
        #endregion

        public async Task<RequestResponse<AttachmentResponse>> GetSingle(GetSingleItemRequest request)
        {
            var errors = new List<Error>();
            var response = await _repository.GetSingle(request);

            if (response.AttachmentId == 0)
            {
                errors.Add(AttachmentErrors.NotFound);
                errors.Add(AttachmentErrors.BadRequest);
                return new RequestResponse<AttachmentResponse>(response, Result.Failure(errors));
            }

            return new RequestResponse<AttachmentResponse>(response, Result.Success());
        }

        #region Update

        public async Task<RequestResponse<AttachmentResponse>> Update(UpdateAttachmentsRequest request)
        {
            var errors = new List<Error>();
            if (request.TypeId == 0)
            {
                errors.Add(AttachmentErrors.BadRequest);
                return new RequestResponse<AttachmentResponse>(new AttachmentResponse(), Result.Failure(errors));
            }

            var response = await _repository.Update(request);
            if (response.TypeId == 0)
            {
                errors.Add(AttachmentErrors.NotFound);
                return new RequestResponse<AttachmentResponse>(new AttachmentResponse(), Result.Failure(errors));
            }

            return new RequestResponse<AttachmentResponse>(new AttachmentResponse(), Result.Success());
        }

        public async Task<RequestResponse<AttachmentResponse>> UpdateTaskAttachments(UpdateAttachmentsRequest request)
        {
            var errors = new List<Error>();
            if (request.TypeId == 0)
            {
                errors.Add(AttachmentErrors.BadRequest);
                return new RequestResponse<AttachmentResponse>(new AttachmentResponse(), Result.Failure(errors));
            }

            var response = await _repository.UpdateTaskAttachments(request);
            if (response.TypeId == 0)
            {
                errors.Add(AttachmentErrors.NotFound);
                return new RequestResponse<AttachmentResponse>(new AttachmentResponse(), Result.Failure(errors));
            }

            return new RequestResponse<AttachmentResponse>(new AttachmentResponse(), Result.Success());
        }

        public async Task<RequestResponse<AttachmentResponse>> UpdateIssueAttachments(UpdateAttachmentsRequest request)
        {
            var errors = new List<Error>();
            if (request.TypeId == 0)
            {
                errors.Add(AttachmentErrors.BadRequest);
                return new RequestResponse<AttachmentResponse>(new AttachmentResponse(), Result.Failure(errors));
            }

            var response = await _repository.UpdateIssueAttachments(request);
            if (response.TypeId == 0)
            {
                errors.Add(AttachmentErrors.NotFound);
                return new RequestResponse<AttachmentResponse>(new AttachmentResponse(), Result.Failure(errors));
            }

            return new RequestResponse<AttachmentResponse>(new AttachmentResponse(), Result.Success());
        }
        #endregion


        #endregion
    }
}
