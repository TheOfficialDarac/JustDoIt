using JustDoIt.Common;
using JustDoIt.Model;
using JustDoIt.Model.DTOs.Requests.Auth;
using JustDoIt.Model.DTOs.Responses;
using JustDoIt.Service.Abstractions;
using JustDoIt.Service.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Azure;

namespace JustDoIt.Service.Implementations
{
    public class UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration) : IUserService
    {
        #region Properties

        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IConfiguration _configuration = configuration;

        #endregion

        #region Methods

        public async Task<RequestResponse<bool>> Create(UserRegistrationRequest data)
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
            if (response.Succeeded) return new RequestResponse<bool>(Result.Success());

            var responseErrors = response.Errors.Select(x => new Error(x.Code, x.Description)).ToList();
            errors.AddRange(responseErrors);
            return Result.Failure(errors);
        }

        public async Task<RequestResponse<string>> LoginAsync(UserLoginRequest request)
        {
            List<Error> errors = [];
            ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                errors.Add(UserErrors.NotFound);
                return new RequestResponse<string>("", Result.Failure(errors));
            }

            var userHasPw = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!userHasPw)
            {
                errors.Add(UserErrors.InvalidCredentials);
                return new RequestResponse<string>("", Result.Failure(errors));
            } 
                
            await _signInManager.SignInAsync(user, false);

            var tokenString = new TokenProvider(_configuration).Create(user);
            return new RequestResponse<string>(tokenString, Result.Success());
        }

        public async Task<Result> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return Result.Success();
        }
        #endregion
    }
}
