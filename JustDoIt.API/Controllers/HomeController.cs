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
        public Task<IEnumerable<Model.Task>> Test() { 
            var response = _service.GetTasks();
            return ((Task<IEnumerable<Model.Task>>)response);
        }
    }
}
