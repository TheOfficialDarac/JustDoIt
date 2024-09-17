using JustDoIt.API.Contracts;
using JustDoIt.Model;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests.Abstractions;
using JustDoIt.Model.DTOs.Requests.Projects;
using JustDoIt.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        [HttpGet(ApiRoutes.Projects.UserProjects)]
        public async Task<IActionResult> GetUserProjects([FromQuery] GetSingleUserRequest request) { 
            try
            {
                
                var result = await _service.GetUserProjects(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet(ApiRoutes.Projects.CurrentUserProjects)]
        public async Task<IActionResult> GetUserProjects() { 
            try
            {
                var request = new GetSingleUserRequest { Id = HttpContext.GetUserId()! };
                var result = await _service.GetUserProjects(request);

                return Ok(result);
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
                var result = await _service.GetAll(searchParams);

                return Ok(result);
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

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut(ApiRoutes.Projects.Update)]
        public async Task<ActionResult> UpdateProject([FromBody] UpdateProjectRequest request)
        {
            try
            {
                var success = await _service.Update(request);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(ApiRoutes.Projects.Create)]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            try
            {
                request.IssuerId = HttpContext.GetUserId();
                var response = await _service.Create(request);

                return Ok(response);
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
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }
}