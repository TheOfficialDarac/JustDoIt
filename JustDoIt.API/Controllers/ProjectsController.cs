using JustDoIt.API.Contracts;
using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Projects;
using JustDoIt.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [Authorize]
    [ApiController, Route(ApiRoutes.Projects.Controller)]
    public class ProjectsController(IProjectService service) : Controller
    {

        #region Properties

        private readonly IProjectService _service = service;

        #endregion Properties

        #region Methods

        //[HttpGet(ApiRoutes.Projects.UserProjects)]
        //public async Task<IActionResult> GetUserProjects([FromQuery] GetSingleUserRequest request) { 
        //    try
        //    {
                
        //        var response = await _service.GetUserProjects(request);

        //        return Ok(new { data = response.ListOfData, result = response.Result });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [Authorize]
        [HttpGet(ApiRoutes.Projects.CurrentUserProjects)]
        public async Task<IActionResult> GetUserProjects() { 
            try
            {
                var context = HttpContext.GetUserId();
                var request = new GetSingleUserRequest { Id = HttpContext.GetUserId()! };
                var response = await _service.GetUserProjects(request);

                return Ok(new { data = response.ListOfData, result = response.Result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(ApiRoutes.Projects.GetAll)]
        public async Task<IActionResult> GetProjects([FromQuery]GetProjectsRequest searchParams
        )
        {
            try
            {
                var response = await _service.GetAll(searchParams);

                return Ok(new { data = response.ListOfData, result = response.Result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(ApiRoutes.Projects.Get)]
        public async Task<ActionResult> GetProject(GetSingleItemRequest request)
        {
            try
            {
                var response = await _service.GetSingle(request);

                return Ok(new { data = response.Data, result = response.Result });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut(ApiRoutes.Projects.Update)]
        public async Task<ActionResult> UpdateProject([FromForm] UpdateProjectRequest request)
        {
            try
            {
                var response = await _service.Update(request);
                return Ok(new { data = response.Data, result = response.Result });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(ApiRoutes.Projects.Create)]
        public async Task<IActionResult> CreateProject([FromForm] CreateProjectRequest request)
        {
            try
            {
                request.IssuerId = HttpContext.GetUserId();
                var response = await _service.Create(request);

                return Ok(new { data=response.Data, result=response.Result});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete(ApiRoutes.Projects.Delete)]
        public async Task<IActionResult> DeleteProject([FromBody] GetSingleItemRequest request)
        {
            try
            {
                var response = await _service.Delete(request);
                return Ok(new { data = response.Data, result = response.Result });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet(ApiRoutes.Projects.CurrentUserProjectRole)]
        public async Task<IActionResult> GetUserRoleInProjectAsync([FromQuery]GetProjectRoleRequest request)
        {
            request.UserId = HttpContext.GetUserId();
            var response = await _service.GetUserRoleInProjectAsync(request);
            if (response.Result.IsSuccess) return Ok(new { data = response.Data, result = response.Result });
            else return BadRequest(new { data = response.Data, result = response.Result });
        }
        #endregion
    }
}