using JustDoIt.Model.DTOs;
using Riok.Mapperly.Abstractions;

namespace JustDoIt.Mapperly
{
    [Mapper]
    public partial class MapperlyMapper
    {
        public partial TaskDTO TaskToTaskDTO(Model.Task task);
        public partial Model.Task TaskDTOToTask(TaskDTO taskDTO);
    }
}
