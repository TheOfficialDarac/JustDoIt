using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests.Attachments;
using JustDoIt.Model.DTOs.Responses.Abstractions;
using JustDoIt.Model.DTOs.Responses.Attachments;
using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Tasks;
using JustDoIt.Model.Responses;
using JustDoIt.Model.Responses.Tasks;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Repository.Mappers;
using JustDoIt.Service.Abstractions;
using JustDoIt.Service.Errors;

namespace JustDoIt.Service.Implementations
{
    public class TaskService(ITaskRepository repository) : ITaskService
    {
        #region Properties

        private readonly ITaskRepository _repository = repository;

        #endregion

        #region Methods

        public async Task<RequestResponse<TaskResponse>> GetUserTasks(GetSingleUserRequest request)
        {
            var errors = new List<Error>();

            var result = await _repository.GetUserTasks(request);

            return new RequestResponse<TaskResponse>(result, Result.Success());
        }

        public async Task<RequestResponse<TaskResponse>> GetAll(GetTasksRequest request)
        {
            var errors = new List<Error>();

            var result = await _repository.GetAll(request);

            return new RequestResponse<TaskResponse>(result, Result.Success());
        }

        public async Task<RequestResponse<CreateTaskResponse>> Create(CreateTaskRequest request)
        {
            var errors = new List<Error>();
            var data = await _repository.Create(request);

            if (data.Id == 0) return new RequestResponse<CreateTaskResponse>(data, Result.Success());

            errors.Add(TaskErrors.NotFound);
            return new RequestResponse<CreateTaskResponse>(data, Result.Failure(errors));
        }

        public async Task<RequestResponse<TaskResponse>> GetSingle(GetSingleItemRequest request)
        {
            var data = await _repository.GetSingle(request);
            if (data.Id.HasValue)
            {
                return new RequestResponse<TaskResponse>(data, Result.Success());
            }
            var errors = new List<Error> { TaskErrors.NotFound };
            return new RequestResponse<TaskResponse>(new TaskResponse(), Result.Failure(errors));
        }

        public async Task<RequestResponse<TaskResponse>> Update(UpdateTaskRequest request)
        {
            //TODO sanitize
            var errors = new List<Error>();

            //TODO checks done
            var data = await _repository.Update(request);
            if (data.Id != 0)
            {
                return new RequestResponse<TaskResponse>(data, Result.Success());
            }

            errors.Add(TaskErrors.NotFound);
            return new RequestResponse<TaskResponse>(data, Result.Failure(errors));
        }

        public async Task<RequestResponse<TaskResponse>> Delete(GetSingleItemRequest request)
        {
            //TODO sanitize
            var errors = new List<Error>();

            //TODO checks done
            var response = await _repository.Delete(request);

            if (response.Id == 0) return new RequestResponse<TaskResponse>(response, Result.Success());

            errors.Add(TaskErrors.NotFound);
            return new RequestResponse<TaskResponse>(response, Result.Failure(errors));

        }

        public async Task<RequestResponse<TaskResponse>> GetUserProjectTasks(GetUserProjectTasksRequest request)
        {
            var errors = new List<Error>();

            var data = await _repository.GetUserProjectTasks(request);

            return new RequestResponse<TaskResponse>(data, Result.Success());
        }
        #endregion
    }
}
