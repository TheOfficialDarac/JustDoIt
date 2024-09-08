using JustDoIt.Model;
using JustDoIt.Model.DTOs;
using Riok.Mapperly.Abstractions;

namespace JustDoIt.Mapperly
{
    [Mapper]
    public partial class ProjectMapper
    {
        public partial ProjectDTO MapToDTO(Project item);
        public partial IEnumerable<ProjectDTO> MapToDTOList(IEnumerable<Project> items);
        public partial Project MapToType(ProjectDTO dto);
        public partial IEnumerable<Project> MapToTypeList(IEnumerable<ProjectDTO> dtos);
    }
}
