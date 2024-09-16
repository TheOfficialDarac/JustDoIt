using JustDoIt.API.Contracts;
using JustDoIt.Model.DTOs.Requests.Auth;
using JustDoIt.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace JustDoIt.API.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Auth.Controller)]
    public class AuthController(IUserService service) : ControllerBase
    {
        private readonly IUserService _service = service;


        [HttpPost(ApiRoutes.Auth.Register)]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationRequest request)
        {
            var response = await _service.RegisterAsync(request);

            if (response.IsSuccess) return NoContent();

            return BadRequest(response);
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            var response = await _service.LoginAsync(request);

            if(response.result.IsSuccess) return Ok(response.data);
            return BadRequest(response.result);
        }

        [Authorize]
        [HttpPost(ApiRoutes.Auth.Logout)]
        public async Task<IActionResult> LogoutAsync()
        {
            await _service.LogoutAsync();
            return NoContent();
        }

        [HttpGet(ApiRoutes.Auth.Test)]
        [Authorize]
        public IActionResult GetHit() => Ok("We get hit");

        [HttpPost(ApiRoutes.Auth.VerifyEmail)]
        public System.Threading.Tasks.Task VerifyEmail() { 
            return System.Threading.Tasks.Task.CompletedTask;
        }

    }
}
