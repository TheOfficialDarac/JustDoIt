using JustDoIt.DAL;
using JustDoIt.Mapperly;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        #region Properties
        private ProjectMapper _mapper {  get; set; }
        private ApplicationContext _context { get; set; }
        #endregion
        public ProjectRepository(ApplicationContext context)
        {
            _context = context;
            _mapper = new ProjectMapper();
        }
        
        public async Task<ProjectDTO?> Create(ProjectDTO entity, string userID)
        {
            var project = _mapper.MapToType(entity);

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            await _context.UserProjects.AddAsync(new Model.UserProject
            {
                UserId = userID,
                ProjectId = project.Id,
                IsVerified = true,
                Token = "CREATOR",
                RoleId = 1
            });

            return _mapper.MapToDTO(project);

        }

        public async Task<ProjectDTO?> Delete(ProjectDTO entity)
        {
            var project = _mapper.MapToType(entity);

            var found = await _context.Projects.FindAsync(project);
            if(found != null)
            {
                _context.Remove(found);
                await _context.SaveChangesAsync();
                return null;
            }

            return entity;
        }

        public Task<IEnumerable<ProjectDTO>?> GetAll(ProjectSearchParams searchParams)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectDTO>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDTO?> GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectDTO>?> GetUserProjects(string userID)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDTO?> Update(ProjectDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDTO?> Create(ProjectDTO entity)
        {
            return null;
            throw new NotImplementedException();
        }
    }
}
