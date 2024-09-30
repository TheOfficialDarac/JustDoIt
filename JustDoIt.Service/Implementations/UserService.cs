using JustDoIt.Common;
using JustDoIt.Service.Abstractions;
using JustDoIt.Service.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using JustDoIt.Model.Database;
using JustDoIt.Model.Requests.Auth;
using JustDoIt.Model.Responses;
using JustDoIt.Model.Responses.Auth;

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

            if (response == null)
            {
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

        public async Task<RequestResponse<string>> UpdateUser(UpdateUserRequest request)
        {
            var errors = new List<Error>();
            if (string.IsNullOrEmpty(request.Id))
            {
                errors.Add(UserErrors.UserIdNotSet);
                return new RequestResponse<string>("", Result.Failure(errors));
            }

            var foundUser = await _userManager.FindByIdAsync(request.Id);
            if (foundUser == null)
            {
                errors.Add(UserErrors.NotFound);
                return new RequestResponse<string>("", Result.Failure(errors));
            }

            if (!string.IsNullOrEmpty(request.FirstName))
                foundUser.FirstName = request.FirstName;

            if (!string.IsNullOrEmpty(request.LastName))
                foundUser.LastName = request.LastName;

            if (!string.IsNullOrEmpty(request.Email))
            {
                foundUser.Email = request.Email;
                foundUser.NormalizedEmail = request.Email.ToUpper();
            }

            if (!string.IsNullOrEmpty(request.FirstName))
            {
                foundUser.UserName = request.UserName;
                foundUser.NormalizedUserName = request.UserName.ToUpper();
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
                foundUser.PhoneNumber = request.PhoneNumber;


            if (!string.IsNullOrEmpty(request.PictureUrl))
                foundUser.PictureUrl = request.PictureUrl;
                    //SaveImageFromPath(request.PictureUrl, request.AttachmentId);

            //return new RequestResponse<string>("", Result.Success());

            await _userManager.GenerateConcurrencyStampAsync(foundUser);
            await _userManager.UpdateAsync(foundUser);
            return new RequestResponse<string>("", Result.Success());
        }

        //! https://learn.microsoft.com/en-us/dotnet/api/system.drawing.image.save?view=net-8.0&redirectedfrom=MSDN#System_Drawing_Image_Save_System_String_System_Drawing_Imaging_ImageFormat_
        //public static string SaveImageFromPath(string url, string userId)
        //{
        //    url = url.Replace("blob:", "");
        //    var path = $"{Directory.GetCurrentDirectory().Replace("//", "/")}/Images/{userId}.";

        //    var extension = url.Split(".")[1];
        //    using (var client = new WebClient())
        //    {
        //        //client.DownloadProgressChanged += client_DownloadProgressChanged;
        //        //client.DownloadFileCompleted += client_DownloadFileCompleted;
        //        client.DownloadFile(new Uri(url), 
        //            $"{path}{extension}");
        //    }
        //    return path;
        //}

        #endregion
    }
}
