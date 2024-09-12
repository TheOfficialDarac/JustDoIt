
using JustDoIt.DAL;
using JustDoIt.Model;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Service.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace JustDoIt.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ProjectController : Controller
    {

        #region Properties

        private readonly IProjectService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion Properties

        #region Contructors

        public ProjectController(IProjectService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }
        #endregion Contructors

        #region Methods

        [HttpGet("user-projects", Name = "GetUserProjects")]
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

        [HttpGet("", Name = "GetProjects")]
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

        [HttpGet("{id:int}", Name = "GetProject")]
        public async Task<ActionResult> GetProject(int id)
        {
            try
            {
                var response = await _service.GetSingle(id);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update", Name = "UpdateProject")]
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

        [HttpPost("create", Name = "CreateProject")]
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

        [HttpDelete("delete", Name = "DeleteProject")]
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