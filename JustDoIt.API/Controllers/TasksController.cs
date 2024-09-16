using Azure;
using JustDoIt.API.Contracts;
using JustDoIt.Model;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests.Tasks;
using JustDoIt.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [ApiController, Route(ApiRoutes.Tasks.Controller)]
    public class TasksController : Controller
    {
        #region Properties

        private readonly ITaskService _service;
        //private readonly UserManager<ApplicationUser> _userManager;
        #endregion Properties

        #region Constructors

        public TasksController(ITaskService service/*, UserManager<ApplicationUser> userManager*/)        {
            _service = service;
            //_userManager = userManager;
        }
        #endregion Constructors

        //#region Methods
        [Authorize]

        [HttpGet(ApiRoutes.Tasks.GetAll)]
        public async Task<ActionResult> GetAll([FromQuery]GetTasksRequest searchParams)
        {
            try
            {
                var response = await _service.GetAll(searchParams);

                return response.result.IsSuccess ? Ok(response.ToTuple() ) : NotFound(response.ToTuple());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpGet(ApiRoutes.Tasks.UserTasks)]
        //public async Task<ActionResult> GetUserTasks()
        //{
        //    try
        //    {
        //        var usr = await _userManager.GetUserAsync(User);
        //        string? id = usr?.Id;

        //        var response = await _service.GetUserTasks(id);

        //        return response is null ? NotFound() : Ok(response);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        [Authorize]
        [HttpGet(ApiRoutes.Tasks.UserProjectTasks)]
        public async Task<ActionResult> GetUserProjectTasks([FromRoute] int projectId)
        {
            try
            {
                var response = await _service.GetUserProjectTasks(HttpContext.GetUserId(), projectId);

                return response.result.IsSuccess ? Ok(response.ToTuple()) : NotFound(response.ToTuple());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet(ApiRoutes.Tasks.Get)]
        public async Task<ActionResult> GetTask(int taskId)
        {
            try
            {
                var response = await _service.GetSingle(taskId);

                return response.result.IsSuccess ? Ok(response.ToTuple()) : NotFound(response.ToTuple());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut(ApiRoutes.Tasks.Update)]
        public async Task<ActionResult> UpdateTask([FromRoute] int taskId, [FromBody] UpdateTaskRequest request)
        {
            try
            {

                if (request is null) return BadRequest();

                var success = await _service.Update(new TaskDTO
                {
                    Id = taskId,
                    Title = request.Title,
                    Summary = request.Summary,
                    Description= request.Description,
                    PictureUrl = request.PictureUrl,
                    Deadline = request.Deadline,
                    IsActive = request.IsActive,
                    State= request.State
                });
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(ApiRoutes.Tasks.Create)]
        public async Task<IActionResult> CreateTask([FromBody]CreateTaskRequest request)
        {
            try
            {
                if (request is null)
                {
                    return NotFound();
                }
                else if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var model = new TaskDTO
                    {
                        Title = request.Title,
                        Summary = request.Summary,
                        Description = request.Description,
                        ProjectId = request.ProjectId,
                        PictureUrl = request.PictureUrl,
                        Deadline = request.Deadline,
                    };
                    var response = await _service.Create(model);

                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                    var locationUrl = $"{baseUrl}/{ApiRoutes.Tasks.Get.Replace("{taskId}", response.data.Id.ToString())}";
                    return Created(locationUrl, response.ToTuple());
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete(ApiRoutes.Tasks.Delete)]
        public async Task<IActionResult> DeleteTask([FromBody]TaskDTO dto)
        {
            try
            {
                var response = await _service.Delete(dto);
                return response.IsSuccess ? NoContent(): NotFound(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //#endregion

    }
}