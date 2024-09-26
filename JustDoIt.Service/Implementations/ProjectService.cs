using JustDoIt.Common;
using JustDoIt.Model.DTOs.Requests.Abstractions;
using JustDoIt.Model.DTOs.Requests.Projects;
using JustDoIt.Model.DTOs.Responses;
using JustDoIt.Model.DTOs.Responses.Projects;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Service.Abstractions;
using JustDoIt.Service.Errors;

namespace JustDoIt.Service.Implementations
{
    public class ProjectService(IProjectRepository repository) : IProjectService
    {
        #region Properties

        private readonly IProjectRepository _repository = repository;

        #endregion

        #region Methods

        public async Task<RequestResponse<ProjectResponse>> GetAll(GetProjectsRequest request)
        {
            var errors = new List<Error>();

            var response = await _repository.GetAll(request);

            if (response.Any()) return new RequestResponse<ProjectResponse>(response, Result.Success());
            
            errors.Add(ProjectErrors.NotFound);
            //errors.Add(ProjectErrors.BadRequest);

            return new RequestResponse<ProjectResponse>([], Result.Failure(errors));

        }

        public async Task<RequestResponse<ProjectResponse>> GetSingle(GetSingleItemRequest request)
        {
            var errors = new List<Error>();
            var response = await _repository.GetSingle(request);

            if (response.Id == 0)
            {
                errors.Add(ProjectErrors.NotFound);
                errors.Add(ProjectErrors.BadRequest);
                return new RequestResponse<ProjectResponse>(response, Result.Failure(errors));
            }

            return new RequestResponse<ProjectResponse>(response, Result.Success());
        }

        public async Task<RequestResponse<ProjectResponse>> GetUserProjects(GetSingleUserRequest request)
        {
            var errors = new List<Error>();
            if (string.IsNullOrEmpty(request.Id.ToString()))
            {
                errors.Add(ProjectErrors.BadRequest);
                return new RequestResponse<ProjectResponse>([], Result.Failure(errors));
            }
            var response = await _repository.GetUserProjects(request);

            if (response.Any())
            {
                return new RequestResponse<ProjectResponse>(response, Result.Success());
            }

            errors.Add(ProjectErrors.NotFound);
            return new RequestResponse<ProjectResponse>([], Result.Failure(errors));
        }

        public async Task<RequestResponse<ProjectResponse>> Update(UpdateProjectRequest request)
        {
            var errors = new List<Error>();
            if (request.Id == 0)
            {
                errors.Add(ProjectErrors.BadRequest);
                return new RequestResponse<ProjectResponse>(new ProjectResponse(), Result.Failure(errors));
            }

            var result = await _repository.Update(request);
            if (result.Id == 0)
            {
                errors.Add(ProjectErrors.NotFound);
                return new RequestResponse<ProjectResponse>(new ProjectResponse(), Result.Failure(errors));
            }

            return new RequestResponse<ProjectResponse>(result, Result.Success());
        }

        public async Task<RequestResponse<CreateProjectResponse>> Create(CreateProjectRequest request)
        {
            var result = await _repository.Create(request);
            if (result.Id != 0)
            {
                return new RequestResponse<CreateProjectResponse>(result,Result.Success());
            }
            var errors = new List<Error> { ProjectErrors.NotFound };

            return new RequestResponse<CreateProjectResponse>(new CreateProjectResponse(), Result.Failure(errors));
        }

        public async Task<RequestResponse<ProjectResponse>> Delete(GetSingleItemRequest request)
        {
            var errors = new List<Error>();
            var result = await _repository.Delete(request);
            if (result.Id == 0)
            {
                return new RequestResponse<ProjectResponse>(result, Result.Success());
            }
            errors.Add(ProjectErrors.NotFound);
            return new RequestResponse<ProjectResponse>(result, Result.Failure(errors));
        }

        public async Task<RequestResponse<GetProjectRoleResponse>> GetUserRoleInProjectAsync(GetProjectRoleRequest request)
        {
            //perform checks
            var errors = new List<Error>();

            var response = await _repository.GetUserRoleInProjectAsync(request);

            return new RequestResponse<GetProjectRoleResponse>(response, Result.Success());
        }
        #endregion
    }
}
