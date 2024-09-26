using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests.Abstractions;
using JustDoIt.Model.DTOs.Requests.Attachments;
using JustDoIt.Model.DTOs.Requests.Tasks;
using JustDoIt.Model.DTOs.Responses;
using JustDoIt.Model.DTOs.Responses.Attachments;
using JustDoIt.Model.DTOs.Responses.Tasks;
using JustDoIt.Service.Abstractions.Common;

namespace JustDoIt.Service.Abstractions
{
    public interface ITaskService : IGenericService<TaskResponse, CreateTaskRequest, CreateTaskResponse, GetTasksRequest, GetSingleItemRequest, UpdateTaskRequest>
    {
        Task<RequestResponse<TaskResponse>> GetUserTasks(GetSingleUserRequest request);
        //Task<(IEnumerable<TaskDTO> data, Result result)> GetAll(GetTasksRequest searchParams);
        Task<RequestResponse<TaskResponse>> GetUserProjectTasks(GetUserProjectTasksRequest request);
    }
}
