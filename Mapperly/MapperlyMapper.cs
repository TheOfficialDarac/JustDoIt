using JustDoIt.API.ViewModel;
using Riok.Mapperly.Abstractions;

namespace JustDoIt.API
{
    [Mapper]
    public partial class MapperlyMapper
    {
        public partial TaskDTO TaskToTaskDTO(Model.Task task);
        public partial Model.Task TaskDTOToTask(TaskDTO taskDTO);
    }
}
