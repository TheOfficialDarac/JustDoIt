using JustDoIt.Model;
using JustDoIt.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [ApiController, Route("api/users")]
    public class AppUserController : Controller {
        #region Properties

        private IService _service { get; set; }
        #endregion Properties

        #region Constructors

        public AppUserController(IService service)
        {
            _service = service;
        }
        #endregion Constructors

        #region Methods

        [HttpGet("", Name = "GetUsers")]
        public async Task<ActionResult> GetUsers(
           string? username, 
            string? firstName, 
            string? lastName, 
            string? email, 
            string? pictureURL, 
            int page = 1, 
            int pageSize = 5) {

            //TODO(Dario)   sanitize possible input scenarios
            
            try {

            var response = await _service.GetUsers(
                username:username,
                firstName:firstName,
                lastName:lastName,
                email:email,
                pictureURL:pictureURL,
                page:page,
                pageSize:pageSize
            );

            return (response is null) ? NotFound() : Ok(response);
            } catch (Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        public async Task<ActionResult> GetUser(int id) {
            try {

            var task = await _service.GetUser(id);
            
            return task is null ? NotFound() : Ok(task);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update", Name = "UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody]AppUser user) {
            try {

                if (user == null) {
                    return NotFound();
                }

                var success = await _service.UpdateUser(user);
                return Ok(success);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create", Name = "CreateUser")]
        public async Task<IActionResult> CreatCreateUser([FromBody] AppUser user) {
            try
            {
                if(user is null) {
                    return NotFound();
                }

                var success = await _service.CreateUser(user);
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