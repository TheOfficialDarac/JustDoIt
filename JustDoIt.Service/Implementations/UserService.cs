using JustDoIt.Common;
using JustDoIt.Model;
using JustDoIt.Model.DTOs.Requests.Auth;
using JustDoIt.Service.Abstractions;
using JustDoIt.Service.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace JustDoIt.Service.Implementations
{
    public class UserService: IUserService
    {
        #region Properties

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        #endregion

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        #region Methods

        public async Task<Result> RegisterAsync(UserRegistrationRequest data)
        {
            var errors = new List<Error>();
            // TODO perform email & password validation

            if (await _userManager.FindByEmailAsync(data.Email) != null)
            {
                errors.Add(UserErrors.UserExists);
                return Result.Failure(errors);
            }

            var response = await _userManager.CreateAsync(new ApplicationUser { Email = data.Email, UserName = data.Email }, data.Password);

            //  send request to confirm email, otherwise unable to login
            if (response.Succeeded) return Result.Success();

            var responseErrors = response.Errors.Select(x => new Error(x.Code, x.Description)).ToList();
            errors.AddRange(responseErrors);
            return Result.Failure(errors);
        }

        public async Task<(string data, Result result)> LoginAsync(UserLoginRequest request)
        {
            List<Error> errors = [];
            ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                errors.Add(UserErrors.NotFound);
                return ("", Result.Failure(errors));
            }

            var userHasPw = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!userHasPw)
            {
                errors.Add(UserErrors.InvalidCredentials);
                return ("", Result.Failure(errors));
            } 
                
            await _signInManager.SignInAsync(user, false);

            var tokenString = new TokenProvider(_configuration).Create(user);
            return (tokenString, Result.Success());
        }

        public async Task<Result> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return Result.Success();
        }
        #endregion
    }
}
