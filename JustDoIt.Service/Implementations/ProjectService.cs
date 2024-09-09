using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Service.Abstractions;
using JustDoIt.Service.Definitions.Common;
using JustDoIt.Service.Errors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<Tuple<ProjectDTO, Result>> Create(ProjectDTO entity)
        {
            return null;
        }

        public async Task<Tuple<IEnumerable<ProjectDTO>?, Result>> GetAll(ProjectSearchParams searchParams)
        {
            var errors = new List<Error>();
            if(searchParams is null)
            {
                errors.Add(ProjectErrors.BadRequest);
                return Tuple.Create(Enumerable.Empty<ProjectDTO>(), Result.Failure(errors));
            }

            var response = await _repository.GetAll(searchParams);
            if (response == null)
            {
                errors.Add(ProjectErrors.NotFound);
                return Tuple.Create(response, Result.Failure(errors));
            }

            return Tuple.Create(response, Result.Success());
        }

        public async Task<Tuple<IEnumerable<ProjectDTO>?, Result>> GetAll()
        {
            var errors = new List<Error>();
            var response = await _repository.GetAll();

            if (response == null)
            {
                errors.Add(ProjectErrors.NotFound);
                errors.Add(ProjectErrors.BadRequest);
                return Tuple.Create(response, Result.Failure(errors));
            }

            return Tuple.Create(response, Result.Success());
        }

        public async Task<Tuple<ProjectDTO, Result>> GetSingle(int id)
        {
            var errors = new List<Error>();
            var response = await _repository.GetSingle(id);

            if(response == null)
            {
                errors.Add(ProjectErrors.NotFound);
                errors.Add(ProjectErrors.BadRequest);
                return Tuple.Create(new ProjectDTO(), Result.Failure(errors));
            }

            return Tuple.Create(response, Result.Success());
        }

        public async Task<Tuple<IEnumerable<ProjectDTO>?, Result>> GetUserProjects(string userID)
        {
            var errors = new List<Error>();
            if (!string.IsNullOrEmpty(userID))
            {
                errors.Add(ProjectErrors.BadRequest);
                return Tuple.Create(Enumerable.Empty<ProjectDTO>(), Result.Failure(errors));
            }
            var response = await _repository.GetUserProjects(userID);

            if (response != null)
            {
                return Tuple.Create(response, Result.Success());
            }

            errors.Add(ProjectErrors.NotFound);
            return Tuple.Create(response, Result.Failure(errors));
        }

        public async Task<Tuple<ProjectDTO?, Result>> Update(ProjectDTO entity)
        {
            var errors = new List<Error>();
            if(entity.Equals(null))
            {
                errors.Add(ProjectErrors.BadRequest);
                return Tuple.Create(entity, Result.Failure(errors));
            }

            var result = await _repository.Update(entity);
            if(result.Equals(null))
            {
                errors.Add(ProjectErrors.NotFound);
                return Tuple.Create(result, Result.Failure(errors));
            }

            return Tuple.Create(result, Result.Success());
        }

        public async Task<Tuple<ProjectDTO?, Result>> Create(ProjectDTO entity, string userID)
        {
            var result = await _repository.Create(entity, userID);
            if (result != null)
            {
                return Tuple.Create(entity, Result.Success());
            }
            var errors = new List<Error> { ProjectErrors.NotFound };

            return Tuple.Create(result, Result.Failure(errors));
        }

        public async Task<Tuple<ProjectDTO, Result>> Delete(ProjectDTO entity)
        {
            var errors = new List<Error>();
            var result = await _repository.Delete(entity);
            if (result.Equals(null))
            {
                return Tuple.Create(result, Result.Success());
            }
            errors.Add(ProjectErrors.NotFound);
            return Tuple.Create(result, Result.Failure(errors));
        }


        #endregion
    }
}
