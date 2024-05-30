using JustDoIt.Model;
using JustDoIt.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [ApiController, Route("api")]
    public class HomeController : Controller
    {
        private IService _service { get; set; }
        public HomeController(IService service)
        {
            _service = service;
        }
        [HttpGet, Route("Test")]
        public Task<string> Index()
        {
            return _service.Test();
        }

        [HttpGet]
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

            if(response is null) {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
