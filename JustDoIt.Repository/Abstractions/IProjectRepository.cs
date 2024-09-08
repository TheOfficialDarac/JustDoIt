using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Repository.Abstractions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Repository.Abstractions
{
    public interface IProjectRepository : IGenericRepository<ProjectDTO>
    {
        Task<ProjectDTO?> Create(ProjectDTO entity, string userID);
        Task<IEnumerable<ProjectDTO>?> GetUserProjects(string userID);
        Task<IEnumerable<ProjectDTO>?> GetAll(ProjectSearchParams searchParams);
    }
}
