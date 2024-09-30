using JustDoIt.API.Contracts;
using JustDoIt.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [ApiController , Authorize]
    [Route(ApiRoutes.Utils.Controller)]
    public class UtilsController(IUtilsService service) : Controller
    {
        private readonly IUtilsService _service = service;

        #region Methods
        [HttpGet(ApiRoutes.Utils.GetAllCategories)]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _service.GetAllCategories();

            return Ok(new { data = response.ListOfData, result = response.Result});
        }

        [HttpGet(ApiRoutes.Utils.GetAllProjectCategories)]
        public async Task<IActionResult> GetAllCategories([FromRoute] int projectId)
        {
            var response = await _service.GetAllProjectCategories(projectId);

            return Ok(new { data = response.ListOfData, result = response.Result });
        }

        [HttpGet(ApiRoutes.Utils.GetAllStatuses)]
        public async Task<IActionResult> GetAllStatuses()
        {
            var response = await _service.GetAllStatuses();

            return Ok(new { data = response.ListOfData, result = response.Result });
        }

        [HttpGet(ApiRoutes.Utils.GetProjectStatus)]
        public async Task<IActionResult> GetProjectStatus([FromRoute] int projectId)
        {
            var response = await _service.GetProjectStatus(projectId);

            return Ok(new { data = response.ListOfData, result = response.Result });
        }

        [HttpGet(ApiRoutes.Utils.GetAllStates)]
        public async Task<IActionResult> GetAllStates()
        {
            var response = await _service.GetAllStates();

            return Ok(new { data = response.ListOfData, result = response.Result });
        }

        [HttpGet(ApiRoutes.Utils.GetTaskState)]
        public async Task<IActionResult> GetTaskState([FromRoute] int taskId)
        {
            var response = await _service.GetTaskState(taskId);

            return Ok(new { data = response.Data, result = response.Result });
        }
        #endregion
    }
}
