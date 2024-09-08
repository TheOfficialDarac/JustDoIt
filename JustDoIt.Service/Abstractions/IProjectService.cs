using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Service.Definitions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Service.Abstractions
{
    public interface IProjectService: IGenericService<ProjectDTO>
    {
        Task<Tuple<ProjectDTO?, Result>> Create(ProjectDTO entity, string userID);
        Task<Tuple<IEnumerable<ProjectDTO>?, Result>> GetUserProjects(string userID);
        Task<Tuple<IEnumerable<ProjectDTO>?, Result>> GetAll(ProjectSearchParams searchParams);
    }
}
