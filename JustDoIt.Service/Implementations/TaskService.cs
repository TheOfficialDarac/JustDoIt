using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Repository.Common;
using JustDoIt.Service.Definitions;

namespace JustDoIt.Service.Implementations
{
    class TaskService : ITaskService
    {
        #region Properties

        private ITaskRepository _repository { get; set; }
        #endregion

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        #region Methods

        public Task<Tuple<IEnumerable<TaskDTO>, Result>> GetUserTasks(string userID)
        {
            throw new NotImplementedException();
        }

        Task<Tuple<IEnumerable<TaskDTO>, Result>> ITaskService.GetAll(string? title, string? description, string? pictureURL, DateTime? deadlineStart, DateTime? deadlineEnd, string? state, string? adminID, int? projectID)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<IEnumerable<TaskDTO>, Result>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result> Create(TaskDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<TaskDTO, Result>> GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Update(TaskDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Delete(TaskDTO entity)
        {
            //!TODO sanitize
            var errors = new List<Error>();
            await _repository.Delete(entity);

            return (errors.Count > 0) ? Result.Failure(errors) : Result.Success();
        }

        #endregion
    }
}
