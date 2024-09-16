using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Service.Abstractions;
using JustDoIt.Service.Errors;

namespace JustDoIt.Service.Implementations
{
    public class TaskService : ITaskService
    {
        #region Properties

        private readonly ITaskRepository _repository;
        #endregion

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        #region Methods

        public async Task<Tuple<IEnumerable<TaskDTO>, Result>> GetUserTasks(string userID)
        {
            var errors = new List<Error>();

            var result = await _repository.GetUserTasks(userID);

            if (result.Any()) return Tuple.Create(result, Result.Success());

            errors.Add(TaskErrors.NotFound);
            return Tuple.Create(result, Result.Failure(errors));

        }

        async Task<Tuple<IEnumerable<TaskDTO>, Result>> ITaskService.GetAll(TaskSearchParams searchParams)
        {
            var errors = new List<Error>();

            var result = await _repository.GetAll(searchParams);

            if (result.Any()) return Tuple.Create(result, Result.Success());

            errors.Add(TaskErrors.NotFound);
            return Tuple.Create(result, Result.Failure(errors));
        }

        public async Task<Tuple<IEnumerable<TaskDTO>, Result>> GetAll()
        {
            var errors = new List<Error>();
            var result = await _repository.GetAll();
            
            if(result.Any()) return Tuple.Create(result, Result.Success());

            errors.Add(TaskErrors.NotFound);
            return Tuple.Create(result, Result.Failure(errors));

        }

        public async Task<Result> Create(TaskDTO entity)
        {
            var errors = new List<Error>();
            var data = await _repository.Create(entity);
            
            if (data == true) return Result.Success();
            
            errors.Add(TaskErrors.NotFound);
            return Result.Failure(errors);
        }

        public async Task<Tuple<TaskDTO, Result>> GetSingle(int id)
        {
            var data = await _repository.GetSingle(id);
            if(data.Id.HasValue)
            {
                return Tuple.Create(data, Result.Success());
            }
            var errors = new List<Error> { TaskErrors.NotFound };
            return Tuple.Create(data, Result.Failure(errors));
        }

        public async Task<Result> Update(TaskDTO entity)
        {
            //TODO sanitize
            var errors = new List<Error>();

            //TODO checks done
            var data = await _repository.Update(entity);
            if (data == true) { 
                return Result.Success(); 
            }

            errors.Add(TaskErrors.NotFound);
            return Result.Failure(errors); 
        }

        public async Task<Result> Delete(TaskDTO entity)
        {
            //TODO sanitize
            var errors = new List<Error>();
            
            //TODO checks done
            var result = await _repository.Delete(entity);

            if (result == true) return Result.Success();
            
            errors.Add(TaskErrors.NotFound);
            return Result.Failure(errors); 
            
        }

        public async Task<Tuple<IEnumerable<TaskDTO>, Result>> GetUserProjectTasks(string userID, int projectID)
        {
            var errors = new List<Error>();

            var data = await _repository.GetUserProjectTasks(userID, projectID);

            if (data.Any())
            {
                return Tuple.Create(data, Result.Success());
            }
            else errors.Add(TaskErrors.NotFound);

            return Tuple.Create(data, Result.Failure(errors));
        }

        #endregion
    }
}
