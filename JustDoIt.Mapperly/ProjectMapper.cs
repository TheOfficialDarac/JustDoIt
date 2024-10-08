﻿using JustDoIt.Model;
using JustDoIt.Model.DTOs.Requests.Projects;
using JustDoIt.Model.DTOs.Responses.Projects;
using Riok.Mapperly.Abstractions;

namespace JustDoIt.Mapperly
{
    [Mapper]
    public partial class ProjectMapper
    {
        //public partial ProjectDTO MapToDTO(Project item);
        //public partial IEnumerable<ProjectDTO> MapToDTOList(IEnumerable<Project> items);
        //public partial Project MapToType(ProjectDTO dto);
        //public partial IEnumerable<Project> MapToTypeList(IEnumerable<ProjectDTO> dtos);

        public partial ProjectResponse ToResponse(Project dto);
        public partial List<ProjectResponse> ToResponseList(List<Project> dtos);
        public partial Project CreateRequestToType(CreateProjectRequest dto);
        public partial CreateProjectResponse TypeToCreateResponse(Project task);
    }
}
