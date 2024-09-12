using JustDoIt.API.Services;
using JustDoIt.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [Route("api/[controller]-v2")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUser user) { 
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (await _authService.Login(user))
            {
                var tokenString = _authService.GenereateTokenString(user);
                return Ok(tokenString);
            }
            return BadRequest();

        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(LoginUser user)
        {
            if(await _authService.RegisterUser(user))
            return Ok("Success.");
            else return BadRequest("Something went wrong.");
        }
    }
}
