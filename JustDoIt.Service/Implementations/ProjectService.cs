using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Service.Abstractions;
using JustDoIt.Service.Errors;
using System;
using System.Collections.Generic;
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

        #region Methods

        public async Task<Tuple<ProjectDTO?, Result>> Create(ProjectDTO entity, string userID)
        {
            var result = await _repository.Create(entity, userID);
            if(result != null)
            {
                return Tuple.Create(entity, Result.Success());
            }
            var errors = new List<Error> { ProjectErrors.NotFound };

            return Tuple.Create(result, Result.Failure(errors));
        }

        public Task<Result> Create(ProjectDTO entity)
        {
            return null;
        }

        public async Task<Result> Delete(ProjectDTO entity)
        {
            var errors = new List<Error>();
            var result = await _repository.Delete(entity);
            if(result.Equals(null))
            {
                return Result.Success();
            }
            errors.Add(ProjectErrors.NotFound);
            return Result.Failure(errors);
        }

        public Task<Tuple<IEnumerable<ProjectDTO>?, Result>> GetAll(ProjectSearchParams searchParams)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<IEnumerable<ProjectDTO>?, Result>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<ProjectDTO?, Result>> GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<IEnumerable<ProjectDTO>?, Result>> GetUserProjects(string userID)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Update(ProjectDTO entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
