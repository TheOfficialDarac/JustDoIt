using JustDoIt.Model.DTOs.Requests.Abstractions;
using JustDoIt.Model.DTOs.Requests.Tasks;
using JustDoIt.Model.DTOs.Responses.Tasks;
using JustDoIt.Repository.Abstractions.Common;

namespace JustDoIt.Repository.Abstractions
{
    public interface ITaskRepository : IGenericRepository<TaskResponse, CreateTaskRequest, CreateTaskResponse, GetTasksRequest, GetSingleItemRequest, UpdateTaskRequest>
    {
        Task<IEnumerable<TaskResponse>> GetUserTasks(GetSingleUserRequest request);

        Task<IEnumerable<TaskResponse>> GetUserProjectTasks(GetUserProjectTasksRequest request);
        Task<IEnumerable<TaskAttachmentResponse>> GetTaskAttachmentsAsync(GetTaskAttachmentsRequest request);
    }
}
