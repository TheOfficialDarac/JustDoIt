using JustDoIt.API.ViewModels;
using JustDoIt.Model;
using Microsoft.AspNetCore.Identity;

namespace JustDoIt.API.Services
{
    public interface IAuthService
    {
        string GenereateTokenString(LoginUser user);
        Task<bool> Login(LoginUser user);
        Task<bool> RegisterUser(LoginUser user);
    }
}