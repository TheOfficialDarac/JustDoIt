using JustDoIt.API.Contracts;
using JustDoIt.Model;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [Authorize]
    [ApiController, Route(ApiRoutes.Projects.Controller)]
    public class ProjectsController : Controller
    {

        #region Properties

        private readonly IProjectService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion Properties

        #region Contructors

        public ProjectsController(IProjectService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }
        #endregion Contructors

        #region Methods

        [HttpGet(ApiRoutes.Projects.UserProjects)]
        public async Task<IActionResult> GetUserProjects() { 
            try
            {
                var usr = await _userManager.GetUserAsync(User);
                //var email = usr?.Email;
                string? id = usr?.Id;
                var result = await _service.GetUserProjects(id);

                return Ok(result);
                //return Ok(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(ApiRoutes.Projects.GetAll)]
        public async Task<IActionResult> GetProjects([FromQuery]ProjectSearchParams searchParams
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
        public async Task<ActionResult> GetProject(int projectId)
        {
            try
            {
                var response = await _service.GetSingle(projectId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut(ApiRoutes.Projects.Update)]
        public async Task<ActionResult> UpdateProject([FromBody] ProjectDTO dto)
        {
            try
            {
                var success = await _service.Update(dto);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(ApiRoutes.Projects.Create)]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDTO dto)
        {
            try
            {
                var usr = await _userManager.GetUserAsync(User);
                string? id = usr?.Id;
                var response = await _service.Create(dto, id);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete(ApiRoutes.Projects.Delete)]
        public async Task<IActionResult> DeleteProject([FromBody] ProjectDTO dto)
        {
            try
            {
                var response = await _service.Delete(dto);
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