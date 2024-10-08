﻿using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Tasks;
using JustDoIt.Model.Responses;
using JustDoIt.Model.Responses.Tasks;
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
