using JustDoIt.API.Contracts;
using JustDoIt.Common;
using JustDoIt.Model.Requests.Auth;
using JustDoIt.Model.Responses.Auth;
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

            if (response.Result.IsSuccess) return NoContent();

            return BadRequest(new { data = response.Data, result = response.Result });
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            var response = await _service.LoginAsync(request);

            if (response.Result.IsSuccess) return Ok(new { data = response.Data, result = response.Result });
            return BadRequest(new { data = response.Data, result = response.Result });
        }

        [Authorize]
        [HttpPost(ApiRoutes.Auth.Logout)]
        public async Task<IActionResult> LogoutAsync()
        {
            await _service.LogoutAsync();
            return NoContent();
        }

        [Authorize]
        [HttpGet(ApiRoutes.Auth.Test)]
        public IActionResult GetHit() => Ok("We get hit");

        [Authorize]
        [HttpGet(ApiRoutes.Auth.UserData)]
        public async Task<IActionResult> GetUserDataAsync()
        {
            //var id = HttpContext.GetUserId();
            //return Ok(id);
            var response = await _service.GetCurrentUserData(HttpContext.GetUserId());
            if (response.Result.IsFailure) return BadRequest(new { data = response.Data, result = response.Result });
            return Ok(new { data = response.Data, result = response.Result });
        }

        [HttpPost(ApiRoutes.Auth.VerifyEmail)]
        public System.Threading.Tasks.Task VerifyEmail()
        {
            return System.Threading.Tasks.Task.CompletedTask;
        }

        [HttpPut(ApiRoutes.Auth.Update)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            var uid = HttpContext.GetUserId();
            request.Id = uid;
            var response = await _service.UpdateUser(request);

            if (response.Result.IsSuccess) return Ok(new { data = response.Data, result = response.Result });
            else return BadRequest(new { data = response.Data, result=response.Result });
        }



    }
}
