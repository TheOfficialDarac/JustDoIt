using JustDoIt.API.Identity;
using JustDoIt.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [Route("api/[controller]-v2")]
    [ApiController]
    public class AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration; 
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        [HttpPost("Register")]
        public async Task<ApplicationUser> RegisterAsync(ApplicationUser user)
        {
            await _userManager.CreateAsync(user);
            return user;
        }

        [HttpPost("Login")]
        public async Task<string> LoginAsync([FromBody]UserLoginCredentials userCred)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(userCred.email);

            if(user == null)
            {
                return "fail";
            }

            PasswordHasher<ApplicationUser> hasher = new();
            hasher.HashPassword(user, userCred.password);

            await _signInManager.SignInAsync(user, false);

            //_signInManager.

            var tokenString = new TokenProvider(_configuration).Create(user);
            return tokenString;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetHit() => Ok("We get hit");
    }
    public record UserLoginCredentials(string email, string password);
}
