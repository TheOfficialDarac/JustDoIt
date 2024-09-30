using JustDoIt.DAL;
using JustDoIt.Model;
using JustDoIt.Model.Database;
using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Projects;
using JustDoIt.Model.Responses.Projects;
using JustDoIt.Repository.Abstractions;
using JustDoIt.Repository.Mappers;
using Microsoft.EntityFrameworkCore;

namespace JustDoIt.Repository.Implementations
{
    public class ProjectRepository(ApplicationContext context) : IProjectRepository
    {
        #region Properties
        private readonly ProjectMapper _mapper = new();
        private readonly ApplicationContext _context = context;

        #endregion

        public async Task<CreateProjectResponse> Create(CreateProjectRequest request)
        {
            try
            {
                var project = _mapper.CreateRequestToType(request);

                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();

                await _context.UserProjects.AddAsync(new UserProject
                {
                    UserId = request.IssuerId.ToString(),
                    ProjectId = project.Id,
                    IsVerified = true,
                    Token = "CREATOR",
                    RoleId = 1,
                    IsFavorite = request.IsFavorite
                });

                if (request.Picture != null && request.Picture.Length > 0)
                {
                    var ext = Path.GetExtension(request.Picture.FileName).ToLowerInvariant();
                    var filePath = $"{Directory.GetCurrentDirectory()}\\Assets\\Projects\\{project.Id}{ext}";

                    using (var stream = File.Create(filePath))
                    {
                        await request.Picture.CopyToAsync(stream);
                    }
                    project.PictureUrl = (filePath).Replace("\\", "/");
                }
                await _context.SaveChangesAsync();

                return _mapper.TypeToCreateResponse(project);
            }
            catch (Exception) { /* Logger */ }
            return new CreateProjectResponse();
        }

        public async Task<ProjectResponse> Delete(GetSingleItemRequest request)
        {
            try
            {
                var found = await _context.Projects.FindAsync(request.Id);
                if (found != null)
                {
                    if (!string.IsNullOrEmpty(found.PictureUrl))
                    File.Delete(found.PictureUrl);

                    var categories = await _context.ProjectCategories.Where(x => x.ProjectId == found.Id).ToListAsync();
                    _context.ProjectCategories.RemoveRange(categories);

                    var userProjects = await _context.UserProjects.Where(x => x.ProjectId == found.Id).ToListAsync();
                    _context.UserProjects.RemoveRange(userProjects);

                    _context.Remove(found);
                    await _context.SaveChangesAsync();

                    return new ProjectResponse();
                }

                return new ProjectResponse { Id = request.Id };

            }
            catch (Exception) { }
            return new ProjectResponse { Id = request.Id };
        }

        public async Task<IEnumerable<ProjectResponse>> GetAll(GetProjectsRequest request)
        {
            try
            {
                var query = _context.Projects.AsQueryable();

                if (!string.IsNullOrEmpty(request.Title))
                {
                    query = query.Where(x => x.Title.Contains(request.Title));
                }

                if (!string.IsNullOrEmpty(request.Key))
                {
                    query = query.Where(x => x.Key.Contains(request.Key));
                }

                if (request.StatusId != 0)
                {
                    query = query.Where(x => x.StatusId.Equals(request.StatusId));
                }

                if (request.MinCreatedDate.HasValue)
                {
                    request.MinCreatedDate = DateTime.SpecifyKind(request.MinCreatedDate.Value, DateTimeKind.Utc);
                    query = query.Where(x => x.CreatedDate >= request.MinCreatedDate);
                }

                if (request.MaxCreatedDate.HasValue)
                {
                    request.MaxCreatedDate = DateTime.SpecifyKind(request.MaxCreatedDate.Value, DateTimeKind.Utc);
                    query = query.Where(x => x.CreatedDate <= request.MaxCreatedDate);
                }

                if (request.Category.Any())
                {
                    //retrieve all categories
                    var projectCategories = await _context.ProjectCategories.Where(x => request.Category.Contains(x.Id)).ToListAsync();
                    query = query.Where(x => projectCategories.All(item => x.ProjectCategories.Contains(item)));
                }

                var result = await query.ToListAsync();
                return _mapper.ToResponseList(result);
            }
            catch (Exception) { }
            return [];
        }
        

        public async Task<ProjectResponse> GetSingle(GetSingleItemRequest request)
        {
            try
            {
                var result = await _context.Projects.FindAsync(request.Id);

                return result is null ? new() : _mapper.ToResponse(result);
            }
            catch (Exception) { }
            return new();
        }

        public async Task<IEnumerable<ProjectResponse>> GetUserProjects(GetSingleUserRequest request)
        {
            var result = await _context.UserProjects
                .Where(x => x.UserId == request.Id)
                .Select(x => x.Project)
                .ToListAsync();

            return _mapper.ToResponseList(result);
        }

        public async Task<ProjectResponse> Update(UpdateProjectRequest request)
        {
            try
            {
                var project = await _context.Projects.FindAsync(request.Id);
                if (project is null) return new ProjectResponse();

                if (!string.IsNullOrEmpty(request.Title))
                    project.Title = request.Title;
                
                if (!string.IsNullOrEmpty(request.Key))
                    project.Key = request.Key;

                if (!string.IsNullOrEmpty(request.Description))
                    project.Description = request.Description;

                //if (!string.IsNullOrEmpty(request.PictureUrl))
                //    project.PictureUrl = request.PictureUrl;

                if (request.Attachment != null && request.Attachment.Length > 0 /* && string.IsNullOrEmpty(request.PictureUrl)*/)
                {
                    var ext = Path.GetExtension(request.Attachment.FileName).ToLowerInvariant();
                    var filePath = $"{Directory.GetCurrentDirectory()}\\Assets\\Projects\\{project.Id}{ext}";

                    using (var stream = File.Create(filePath))
                    {
                        await request.Attachment.CopyToAsync(stream);
                    }
                    project.PictureUrl = (filePath).Replace("\\", "/");
                }

                project.StatusId = request.StatusId;

                if (request.Category.Any())
                {
                    //retrieve all categories
                    var projectCategories = await _context.ProjectCategories.Where(x => request.Category.Contains(x.Id)).ToListAsync();
                    project.ProjectCategories = projectCategories;
                }

                await _context.SaveChangesAsync();
                return _mapper.ToResponse(project);
            }
            catch (Exception) { }
            return new();
        }

        public async Task<GetProjectRoleResponse> GetUserRoleInProjectAsync(GetProjectRoleRequest request)
        {
            var foundProject = await _context.Projects.FindAsync(request.ProjectId);
            if (foundProject is null) return new();

            var response = await _context.UserProjects.Where(x => x.UserId == request.UserId && x.ProjectId == request.ProjectId).Where(x => x.IsVerified == true).Select(x => x.Role).FirstOrDefaultAsync();
            if (response is null) return new();

            return _mapper.TypeToGetRoleResponse(response);
        }
    }
}
