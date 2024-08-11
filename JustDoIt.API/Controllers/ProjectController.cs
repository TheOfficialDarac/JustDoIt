using JustDoIt.Model;
using JustDoIt.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ProjectController : Controller
    {

        #region Properties

        private readonly IService _service;
        #endregion Properties

        #region Contructors

        public ProjectController(IService service)
        {
            _service = service;
        }
        #endregion Contructors

        #region Methods

        [HttpGet("users", Name = "GetProjectUsers")]
        public async Task<IActionResult> GetProjectUsers(int projectID)
        {
            try
            {

                var result = await _service.GetProjectUsers(projectID);

                return (result is null) ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("", Name = "GetProjects")]
        public async Task<IActionResult> GetProjects(
            string? title,
            string? description,
            string? pictureURL,
            string? adminID,
            int page = 1,
            int pageSize = 5
        )
        {
            try
            {

                var result = await _service.GetProjects(
                    title: title,
                    description: description,
                    pictureURL: pictureURL,
                    adminID: adminID,
                    page: page,
                    pageSize: pageSize
                );

                return (result is null) ? NotFound() : Ok(result);
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

                var project = await _service.GetProject(id);

                return project is null ? NotFound() : Ok(project);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update", Name = "UpdateProject")]
        public async Task<ActionResult> UpdateProject([FromBody] Project project)
        {
            try
            {

                if (project == null)
                {
                    return NotFound();
                }

                var success = await _service.UpdateProject(project);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create", Name = "CreateProject")]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            try
            {
                if (project is null)
                {
                    return NotFound(project);
                }

                var success = await _service.CreateProject(project);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete", Name = "DeleteProject")]
        public async Task<IActionResult> DeleteProject(int projectID)
        {
            try
            {
                // if(!projectID.HasValue) {
                //     return NotFound();
                // }

                var success = await _service.DeleteProject(projectID);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }
}