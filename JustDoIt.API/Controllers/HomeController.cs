using JustDoIt.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [ApiController, Route("api")]
    public class HomeController : Controller
    {
        #region Properties

        private IService _service { get; set; }
        #endregion Properties

        #region Constructors

        public HomeController(IService service)
        {
            _service = service;
        }
        #endregion Constructors

        #region Methods

        [HttpGet("tasks", Name = "GetTasks")]
        public async Task<ActionResult> GetTasks(
            string? title,
            string? description,
            string? pictureURL,
            DateTime? deadlineStart,
            DateTime? deadlineEnd,
            string? state,
            int? adminID,
            int? projectID,
            int page = 1,
            int pageSize = 5
        ) { 

            //TODO(Dario)   sanitize possible input scenarios
            
            try {

            var response = await _service.GetTasks(
                title: title,
                description: description,
                pictureURL: pictureURL,
                deadlineStart: deadlineStart,
                deadlineEnd: deadlineEnd,
                state: state,
                adminID: adminID,
                projectID: projectID,
                page: page,
                pageSize: pageSize
            );

            return response is null ? NotFound() : Ok(response);
            } catch {
                return BadRequest();
            }
        }

        [HttpGet("tasks/{id:int}", Name = "GetTask")]
        public async Task<ActionResult> GetTask(int id) {
            try {

            var task = await _service.GetTask(id);
            
            return task is null ? NotFound() : Ok(task);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
        #endregion Methods
    }
}