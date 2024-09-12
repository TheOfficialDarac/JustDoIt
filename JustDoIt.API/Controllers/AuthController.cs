using JustDoIt.Model;
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
        public async Task<string> LoginAsync(string email, string password)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return "fail";
            }

            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
            hasher.HashPassword(user, password);

            await _signInManager.SignInAsync(user, false);

            //_signInManager.
            var tokenString = new TokenProvider(_configuration).Create(user);
            return tokenString;
        }
    }
}
