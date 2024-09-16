using JustDoIt.DAL;
using JustDoIt.Mapperly;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests.Projects;
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
        private readonly ProjectMapper _mapper;
        private readonly ApplicationContext _context;
        #endregion
        public ProjectRepository(ApplicationContext context)
        {
            _context = context;
            _mapper = new ProjectMapper();
        }

        public async Task<ProjectDTO> Create(ProjectDTO entity, string userID)
        {
            try
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
            catch
            {
                return new ProjectDTO();
            }
        }

        public Task<ProjectDTO> Create(ProjectDTO entity)
        {
            return Task.FromResult(entity);
        }

        public async Task<bool> Delete(ProjectDTO entity)
        {
            var project = _mapper.MapToType(entity);

            var found = await _context.Projects.FindAsync(project);
            if(found != null)
            {
                _context.Remove(found);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ProjectDTO>> GetAll(GetProjectsRequest searchParams)
        {
            var query = _context.Projects.AsQueryable();

            if(!string.IsNullOrEmpty(searchParams.Title))
            {
                query = query.Where(x => x.Title.Contains(searchParams.Title));
            }

            if(searchParams.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive.Equals(searchParams.IsActive));
            }

            if(searchParams.MinCreatedDate.HasValue)
            {
                searchParams.MinCreatedDate = DateTime.SpecifyKind(searchParams.MinCreatedDate.Value, DateTimeKind.Utc);
                query = query.Where(x => x.CreatedDate >= searchParams.MinCreatedDate);
            }

            if (searchParams.MaxCreatedDate.HasValue)
            {
                searchParams.MaxCreatedDate = DateTime.SpecifyKind(searchParams.MaxCreatedDate.Value, DateTimeKind.Utc);
                query = query.Where(x => x.CreatedDate <= searchParams.MaxCreatedDate);
            }

            var result = await query.ToListAsync();
            return _mapper.MapToDTOList(result);
        }

        public async Task<IEnumerable<ProjectDTO>> GetAll()
        {
            var result = await _context.Projects.ToListAsync();
            return _mapper.MapToDTOList(result);
        }

        public async Task<ProjectDTO> GetSingle(int id)
        {
            var query = _context.Projects.AsQueryable();
            query = query.Where(x => x.Id.Equals(id));

            var result = await query.SingleOrDefaultAsync();
            return result is null ? new ProjectDTO() : _mapper.MapToDTO(result);
        }

        public async Task<IEnumerable<ProjectDTO>> GetUserProjects(string userID)
        { 
            var result = await _context.UserProjects.AsQueryable()
                .Where(x => x.UserId.Equals(userID))
                .Select(x => x.Project)
                .ToListAsync();
            return _mapper.MapToDTOList(result);
        }

        public async Task<bool> Update(ProjectDTO entity)
        {
            var project = _mapper.MapToType(entity);

            var found = await _context.Projects.FindAsync(project);
            if(found is null) return false;

            found.Title = entity.Title!;
            found.Description = entity.Description;
            found.PictureUrl = entity.PictureUrl;
            found.IsActive = entity.IsActive!.Value;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
