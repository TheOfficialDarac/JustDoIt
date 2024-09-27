using JustDoIt.API.Contracts;
using JustDoIt.Model.DTOs.Requests.Attachments;
using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Tasks;
using JustDoIt.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [Authorize]
    [ApiController, Route(ApiRoutes.Tasks.Controller)]
    public class TasksController(ITaskService service) : Controller
    {
        #region Properties

        private readonly ITaskService _service = service;
        #endregion Properties

        #region Methods

        [Authorize]
        [HttpGet(ApiRoutes.Tasks.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetTasksRequest request)
        {
            try
            {
                var response = await _service.GetAll(request);

                return response.Result.IsSuccess ? Ok(new { data = response.ListOfData, result = response.Result }) : BadRequest(new { data = response.ListOfData, result = response.Result });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpGet(ApiRoutes.Tasks.UserTasks)]
        //public Task<IActionResult> GetUserTasks()
        //{
        //return NotFound();
        //try
        //{

        //    var response = await _service.GetUserTasks(request);

        //    return response is null ? NotFound() : Ok(response);
        //}
        //catch (Exception e)
        //{
        //    return BadRequest(e.Message);
        //}
        //}

        [Authorize]
        [HttpGet(ApiRoutes.Tasks.UserProjectTasks)]
        public async Task<IActionResult> GetUserProjectTasks([FromQuery] GetUserProjectTasksRequest request)
        {
            try
            {
                request.UserId = HttpContext.GetUserId();
                var response = await _service.GetUserProjectTasks(request);

                return response.Result.IsSuccess ? Ok(new { data = response.ListOfData, result = response.Result }) : NotFound(new { data = response.ListOfData, result = response.Result });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet(ApiRoutes.Tasks.Get)]
        public async Task<IActionResult> GetTask([FromQuery] GetSingleItemRequest request)
        {
            try
            {
                var response = await _service.GetSingle(request);

                return response.Result.IsSuccess ? Ok(new { data = response.Data, result = response.Result}) : NotFound(new { data = response.Data, result = response.Result });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut(ApiRoutes.Tasks.Update)]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskRequest request)
        {
            try
            {

                if (request is null) return BadRequest();

                var response = await _service.Update(request);
                return Ok(new { data = response.Data, result = response.Result });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(ApiRoutes.Tasks.Create)]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
        {
            try
            {
                var response = await _service.Create(request);

                if (response.Result.IsFailure) return BadRequest(response.Result);

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                var locationUrl = $"{baseUrl}/{ApiRoutes.Tasks.Get.Replace("{taskId}", response.Data!.Id.ToString())}";
                return Created(locationUrl, new { data = response.Data, result = response.Result });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete(ApiRoutes.Tasks.Delete)]
        public async Task<IActionResult> DeleteTask([FromBody] GetSingleItemRequest request)
        {
            try
            {
                var response = await _service.Delete(request);
                return response.Result.IsSuccess ? NoContent() : NotFound(new { data = response.Data, result = response.Result });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

    }
}