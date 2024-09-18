using JustDoIt.Common;
using JustDoIt.Model;
using JustDoIt.Model.DTOs.Requests.Auth;
using JustDoIt.Model.DTOs.Responses;
using JustDoIt.Service.Abstractions;
using JustDoIt.Service.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Azure;
using JustDoIt.Model.DTOs.Responses.Auth;

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

        public async Task<RequestResponse<UserRegistrationResponse>> RegisterAsync(UserRegistrationRequest request)
        {
            var errors = new List<Error>();
            // TODO perform email & password validation

            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                errors.Add(UserErrors.UserExists);
                return new RequestResponse<UserRegistrationResponse>(new UserRegistrationResponse { UserIsCreated = false }, Result.Failure(errors));
            }

            var response = await _userManager.CreateAsync(new ApplicationUser { Email = request.Email, UserName = request.Email, NormalizedEmail = request.Email.ToUpper(), NormalizedUserName = request.Email.ToUpper() }, request.Password);

            //  send request to confirm email, otherwise unable to login
            if (response.Succeeded) return new RequestResponse<UserRegistrationResponse>(new UserRegistrationResponse { UserIsCreated = true }, Result.Success());

            var responseErrors = response.Errors.Select(x => new Error(x.Code, x.Description)).ToList();
            errors.AddRange(responseErrors);
            return new RequestResponse<UserRegistrationResponse>(new UserRegistrationResponse { UserIsCreated = false }, Result.Failure(errors));
        }

        public async Task<RequestResponse<UserLoginResponse>> LoginAsync(UserLoginRequest request)
        {
            List<Error> errors = [];
            ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                errors.Add(UserErrors.NotFound);
                return new RequestResponse<UserLoginResponse>(new UserLoginResponse(), Result.Failure(errors));
            }

            var userHasPw = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!userHasPw)
            {
                errors.Add(UserErrors.InvalidCredentials);
                return new RequestResponse<UserLoginResponse>(new UserLoginResponse(), Result.Failure(errors));
            } 
                
            await _signInManager.SignInAsync(user, request.RememberMe);

            var tokenString = new TokenProvider(_configuration).Create(user);
            return new RequestResponse<UserLoginResponse>(new UserLoginResponse { Token = tokenString }, Result.Success());
        }

        public async Task<Result> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return Result.Success();
        }

        public async Task<RequestResponse<UserResponse>> GetCurrentUserData(string request)
        {
            var errors = new List<Error>();
            var response = await _userManager.FindByIdAsync(request);

            if (response == null) {
                errors.Add(UserErrors.NotFound);
                return new RequestResponse<UserResponse>(new UserResponse(), Result.Failure(errors));
            }
            return new RequestResponse<UserResponse>(new UserResponse
            {
                FirstName = response.FirstName,
                LastName = response.LastName,
                Email = response.Email,
                UserName = response.UserName,
                PhoneNumber = response.PhoneNumber,
                PictureUrl = response.PictureUrl
            }, Result.Success());
        }

        #endregion
    }
}
