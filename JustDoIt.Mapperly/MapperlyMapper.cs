using JustDoIt.Model.DTOs;
using Riok.Mapperly.Abstractions;

namespace JustDoIt.Mapperly
{
    [Mapper]
    public partial class MapperleyMapper
    {
        public partial TaskDTO TaskToTaskDTO(Model.Task task);
        public partial Model.Task TaskDTOToTask(TaskDTO taskDTO);
    }
}
