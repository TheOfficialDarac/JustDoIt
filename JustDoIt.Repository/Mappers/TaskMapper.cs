using JustDoIt.Model.Responses.Attachments;
using JustDoIt.Model.Requests.Tasks;
using JustDoIt.Model.Responses.Tasks;
using Riok.Mapperly.Abstractions;

namespace JustDoIt.Repository.Mappers
{
    [Mapper]
    public partial class TaskMapper
    {

        public partial TaskResponse ToResponse(Model.Database.Task dto);

        public partial List<TaskResponse> ToResponseList(List<Model.Database.Task> dto);

        public partial Model.Database.Task CreateRequestToType(CreateTaskRequest dto);

        public partial CreateTaskResponse TypeToCreateResponse(Model.Database.Task task);
    }
}
