using JustDoIt.Model;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.ViewModels;
using JustDoIt.Service.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class TaskController : Controller
    {
        #region Properties

        private readonly ITaskService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion Properties

        #region Constructors

        public TaskController(ITaskService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }
        #endregion Constructors

        //#region Methods

        [HttpGet("", Name = "GetAll")]
        public async Task<ActionResult> GetAll([FromQuery]TaskSearchParams searchParams)
        {
            try
            {

                var response = await _service.GetAll(searchParams
                );

                return response is null ? NotFound() : Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("user-tasks", Name = "GetUserTasks")]
        public async Task<ActionResult> GetUserTasks()
        {
            try
            {
                var usr = await _userManager.GetUserAsync(User);
                string? id = usr?.Id;
                var response = await _service.GetUserTasks(id);

                return response is null ? NotFound() : Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("user-project-tasks", Name = "GetUserProjectTasks")]
        public async Task<ActionResult> GetUserProjectTasks([FromQuery] int projectID)
        {
            try
            {
                var usr = await _userManager.GetUserAsync(User);
                string? id = usr?.Id;
                var response = await _service.GetUserProjectTasks(id, projectID);

                return response is null ? NotFound() : Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetTask")]
        public async Task<ActionResult> GetTask(int id)
        {
            try
            {

                var task = await _service.GetSingle(id);

                return task is null ? NotFound() : Ok(task);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update", Name = "UpdateTask")]
        public async Task<ActionResult> UpdateTask([FromBody] TaskDTO task)
        {
            try
            {

                if (task == null)
                {
                    return NotFound();
                }

                var success = await _service.Update(task);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create", Name = "CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody]TaskDTO model)
        {
            try
            {
                if (model is null)
                {
                    return NotFound();
                }
                else if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var success = await _service.Create(model);
                    return Ok(success);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete", Name = "DeleteTask")]
        public async Task<IActionResult> DeleteTask(TaskDTO? dto)
        {
            ModelState.Remove("Project");
            try
            {
                var success = await _service.Delete(dto);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //#endregion

    }
}