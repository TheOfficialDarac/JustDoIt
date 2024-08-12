using JustDoIt.Model.DTOs;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Mapperly
{
    [Mapper]
    public partial class TaskMapper
    {
        public partial TaskDTO MapToDTO(Model.Task task);
        public partial IEnumerable<TaskDTO> MapToDTOList(IEnumerable<Model.Task> tasks);
        public partial Model.Task MapToType(TaskDTO taskDTO);
        public partial IEnumerable<Model.Task> MapToTypeList(IEnumerable<TaskDTO> tasksDTOs);
    }
}
