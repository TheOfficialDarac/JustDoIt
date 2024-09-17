using JustDoIt.DAL;
using JustDoIt.Model.DTOs.Requests.Abstractions;
using JustDoIt.Model.DTOs.Requests.Projects;
using JustDoIt.Model.DTOs.Responses.Projects;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Repository.Mappers;
using Microsoft.EntityFrameworkCore;

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

        public async Task<CreateProjectResponse> Create(CreateProjectRequest request)
        {
            try
            {
                var project = _mapper.CreateRequestToType(request);

                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();

                await _context.UserProjects.AddAsync(new Model.UserProject
                {
                    UserId = request.IssuerId.ToString(),
                    ProjectId = project.Id,
                    IsVerified = true,
                    Token = "CREATOR",
                    RoleId = 1
                });

                return _mapper.TypeToCreateResponse(project);
            }
            catch { /* Logger */ }
            return new CreateProjectResponse();
        }

        public async Task<ProjectResponse> Delete(GetSingleItemRequest request)
        {
            var found = await _context.Projects.FindAsync(request.Id);
            if(found != null)
            {
                _context.Remove(found);
                await _context.SaveChangesAsync();
                return new ProjectResponse();
            }

            return new ProjectResponse { Id = request.Id };
        }

        public async Task<IEnumerable<ProjectResponse>> GetAll(GetProjectsRequest request)
        {
            var query = _context.Projects.AsQueryable();

            if(!string.IsNullOrEmpty(request.Title))
            {
                query = query.Where(x => x.Title.Contains(request.Title));
            }

            if(request.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive.Equals(request.IsActive));
            }

            if(request.MinCreatedDate.HasValue)
            {
                request.MinCreatedDate = DateTime.SpecifyKind(request.MinCreatedDate.Value, DateTimeKind.Utc);
                query = query.Where(x => x.CreatedDate >= request.MinCreatedDate);
            }

            if (request.MaxCreatedDate.HasValue)
            {
                request.MaxCreatedDate = DateTime.SpecifyKind(request.MaxCreatedDate.Value, DateTimeKind.Utc);
                query = query.Where(x => x.CreatedDate <= request.MaxCreatedDate);
            }

            var result = await query.ToListAsync();
            return _mapper.ToResponseList(result);
        }

        public async Task<ProjectResponse> GetSingle(GetSingleItemRequest request)
        {
            var result = await _context.Projects.FindAsync(request.Id);

            return result is null ? new() : _mapper.ToResponse(result);
        }

        public async Task<IEnumerable<ProjectResponse>> GetUserProjects(GetSingleUserRequest request)
        { 
            var result = await _context.UserProjects.AsQueryable()
                .Where(x => x.UserId.Equals(request.Id))
                .Select(x => x.Project)
                .ToListAsync();

            return _mapper.ToResponseList(result);        }

        public async Task<ProjectResponse> Update(UpdateProjectRequest request)
        {
            var found = await _context.Projects.FindAsync(request.Id);
            if(found is null) return new ProjectResponse();

            found.Title = request.Title;
            found.Description = request.Description;
            found.PictureUrl = request.PictureUrl;
            found.IsActive = request.IsActive;

            await _context.SaveChangesAsync();
            return _mapper.ToResponse(found);
        }
    }
}
