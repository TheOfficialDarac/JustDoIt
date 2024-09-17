using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests;
using JustDoIt.Model.DTOs.Requests.Tasks;
using JustDoIt.Model.DTOs.Responses.Tasks;
using Riok.Mapperly.Abstractions;

namespace JustDoIt.Repository.Mappers
{
    [Mapper]
    public partial class TaskMapper
    {

        public partial TaskResponse ToResponse(Model.Task dto);

        public partial List<TaskResponse> ToResponseList(List<Model.Task> dto);

        public partial Model.Task CreateRequestToType(CreateTaskRequest dto);

        public partial CreateTaskResponse TypeToCreateResponse(Model.Task task);
    }
}
