using JustDoIt.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.WebAPI.Controllers
{
    [ApiController, Route("api")]
    public class HomeController : Controller
    {
        private IService _service { get; set; }
        public HomeController(IService service)
        {
            _service = service;
        }
        [HttpGet]
        public Task<string> Index()
        {
            return _service.Test();
        }
    }
}
