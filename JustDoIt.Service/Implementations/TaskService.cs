using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Service.Definitions;
using JustDoIt.Service.Errors;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;

namespace JustDoIt.Service.Implementations
{
    public class TaskService : ITaskService
    {
        #region Properties

        private ITaskRepository _repository { get; set; }
        #endregion

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        #region Methods

        public async Task<Tuple<IEnumerable<TaskDTO>?, Result>> GetUserTasks(string userID)
        {
            var data = await _repository.GetUserTasks(userID);
            return Tuple.Create(data, Result.Success());
        }

        async Task<Tuple<IEnumerable<TaskDTO>?, Result>> ITaskService.GetAll(TaskSearchParams searchParams)
        {
                var data = await _repository.GetAll(searchParams);
            return Tuple.Create(data, Result.Success());
        }

        public Task<Tuple<IEnumerable<TaskDTO>?, Result>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<TaskDTO, Result>> Create(TaskDTO entity)
        {
            var errors = new List<Error>();
            var data = await _repository.Create(entity);
            if (data != null)
            {
                return Tuple.Create(null, Result.Success());
            }
            else errors.Add(TaskErrors.NotFound);

            return Tuple.Create(entity, Result.Failure(errors));
        }

        public async Task<Tuple<TaskDTO?, Result>> GetSingle(int id)
        {
            var data = await _repository.GetSingle(id);
            if(data != null)
            {
                return Tuple.Create(data, Result.Success());
            }
            var errors = new List<Error> { TaskErrors.NotFound };
            return Tuple.Create(data, Result.Failure(errors));
        }

        public async Task<Tuple<TaskDTO, Result>> Update(TaskDTO entity)
        {
            var data = await _repository.Update(entity);
            if (data != null) { 
                return Tuple.Create(data, Result.Success()); 
            }
            var errors = new List<Error> { TaskErrors.NotFound };
            return Tuple.Create(entity, Result.Failure(errors)); 
        }

        public async Task<Tuple<TaskDTO, Result>> Delete(TaskDTO entity)
        {
            //!TODO sanitize
            var errors = new List<Error>();
            await _repository.Delete(entity);

            if (errors.Count > 0) { Result.Failure(errors); }
            return Tuple.Create(entity, Result.Success());
        }

        public async Task<Tuple<IEnumerable<TaskDTO>?, Result>> GetUserProjectTasks(string userID, int projectID)
        {
            var errors = new List<Error>();

            var data = await _repository.GetUserProjectTasks(userID, projectID);

            if (data != null && data.Any())
            {
                return Tuple.Create(data, Result.Success());
            }
            else errors.Add(TaskErrors.NotFound);

            return Tuple.Create(data, Result.Failure(errors));
        }

        #endregion
    }
}
