using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests.Projects;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Service.Abstractions;
using JustDoIt.Service.Errors;

namespace JustDoIt.Service.Implementations
{
    public class ProjectService : IProjectService
    {
        #region Properties

        private readonly IProjectRepository _repository;

        #endregion
        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        #region Methods

        public Task<(ProjectDTO data,Result result)> Create(ProjectDTO entity)
        {
            var errors = new List<Error> { };
            return Task.FromResult((new ProjectDTO(),Result.Failure(errors)));
        }

        public async Task<(IEnumerable<ProjectDTO> data, Result result)> GetAll(GetProjectsRequest searchParams)
        {
            var errors = new List<Error>();

            if (searchParams is not null)
            {
                var response = await _repository.GetAll(searchParams);

                if (response.Any()) return (response, Result.Success());
                else errors.Add(ProjectErrors.NotFound);
            }
            errors.Add(ProjectErrors.BadRequest);

            return (Enumerable.Empty<ProjectDTO>(), Result.Failure(errors));

        }

        public async Task<(IEnumerable<ProjectDTO> data, Result result)> GetAll()
        {
            var errors = new List<Error>();
            var response = await _repository.GetAll();

            if (response.Any())
            {
                return (response, Result.Success());
            }

            errors.Add(ProjectErrors.NotFound);
            errors.Add(ProjectErrors.BadRequest);
            return (response, Result.Failure(errors));
        }

        public async Task<(ProjectDTO data, Result result)> GetSingle(int id)
        {
            var errors = new List<Error>();
            var response = await _repository.GetSingle(id);

            if (response.Id.HasValue)
            {
                errors.Add(ProjectErrors.NotFound);
                errors.Add(ProjectErrors.BadRequest);
                return (new ProjectDTO(), Result.Failure(errors));
            }

            return (response, Result.Success());
        }

        public async Task<(IEnumerable<ProjectDTO> data, Result result)> GetUserProjects(string userID)
        {
            var errors = new List<Error>();
            if (string.IsNullOrEmpty(userID))
            {
                errors.Add(ProjectErrors.BadRequest);
                return (Enumerable.Empty<ProjectDTO>(), Result.Failure(errors));
            }
            var response = await _repository.GetUserProjects(userID);

            if (response.Any())
            {
                return (response, Result.Success());
            }

            errors.Add(ProjectErrors.NotFound);
            return (response, Result.Failure(errors));
        }

        public async Task<Result> Update(ProjectDTO entity)
        {
            var errors = new List<Error>();
            if (entity.Equals(null))
            {
                errors.Add(ProjectErrors.BadRequest);
                return Result.Failure(errors);
            }

            var result = await _repository.Update(entity);
            if (result.Equals(null))
            {
                errors.Add(ProjectErrors.NotFound);
                return Result.Failure(errors);
            }

            return Result.Success();
        }

        public async Task<(ProjectDTO data, Result result)> Create(ProjectDTO entity, string userID)
        {
            var result = await _repository.Create(entity, userID);
            if (result.Id.HasValue)
            {
                return (result,Result.Success());
            }
            var errors = new List<Error> { ProjectErrors.NotFound };

            return (new ProjectDTO(), Result.Failure(errors));
        }

        public async Task<Result> Delete(ProjectDTO entity)
        {
            var errors = new List<Error>();
            var result = await _repository.Delete(entity);
            if (result.Equals(null))
            {
                return Result.Success();
            }
            errors.Add(ProjectErrors.NotFound);
            return Result.Failure(errors);
        }


        #endregion
    }
}
