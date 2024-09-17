using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests;
using JustDoIt.Model.DTOs.Requests.Tasks;
using JustDoIt.Model.DTOs.Responses.Tasks;
using Riok.Mapperly.Abstractions;

namespace JustDoIt.Mapperly
{
    [Mapper]
    public partial class TaskMapper
    {
        #region Base Methods

        //public partial TaskDTO ToDto(Model.Task task);
        //public partial IEnumerable<TaskDTO> ToDtoList(IEnumerable<Model.Task> tasks);
        //public partial Model.Task ToType(TaskDTO dto);
        //public partial IEnumerable<Model.Task> ToTypeList(IEnumerable<TaskDTO> tasksDTOs);
        #endregion

        public partial TaskResponse ToResponse(Model.Task dto);
        public partial List<TaskResponse> ToResponseList(List<Model.Task> dto);
        public partial Model.Task CreateRequestToType(CreateTaskRequest dto);
        public partial CreateTaskResponse TypeToCreateResponse(Model.Task task);

        //public partial UpdateTaskResponse ToUpdateDto(Model.Task dto);
        //public partial List<TaskResponse> ToGetList(List<Model.Task> dto);


        //public partial TaskDTO RequestToDto(GetSingleItemRequest request);

    }
}
