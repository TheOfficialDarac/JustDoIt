using JustDoIt.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [ApiController, Route("api/tasks")]
    public class TaskController : Controller
    {
        #region Properties

        private IService _service { get; set; }
        #endregion Properties

        #region Constructors

        public TaskController(IService service)
        {
            _service = service;
        }
        #endregion Constructors

        #region Methods

        [HttpGet("", Name = "GetTasks")]
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
            } catch (Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetTask")]
        public async Task<ActionResult> GetTask(int id) {
            try {

            var task = await _service.GetTask(id);
            
            return task is null ? NotFound() : Ok(task);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update", Name = "UpdateTask")]
        public async Task<ActionResult> PutTask([FromBody]Model.Task task) {
            try {

                if (task == null) {
                    return NotFound();
                }

                var success = await _service.UpdateTask(task);
                return Ok(success);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create", Name = "CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] Model.Task task) {
            try
            {
                if(task is null) {
                    return NotFound();
                }

                var success = await _service.CreateTask(task);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion Methods
    }
}