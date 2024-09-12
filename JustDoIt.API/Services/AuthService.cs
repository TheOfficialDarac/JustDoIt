using JustDoIt.API.ViewModels;
using JustDoIt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JustDoIt.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        public async Task<bool> RegisterUser(LoginUser user)
        {
            var identityUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.UserName
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);
            return result.Succeeded;
        }

        public async Task<bool> Login(LoginUser user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.UserName);
            if (identityUser == null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(identityUser, user.Password);
        }

        public string GenereateTokenString(LoginUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim(ClaimTypes.Role, "User"),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            var signInCred= new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha512Signature);
            
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signInCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}
