using JustDoIt.Common;
using JustDoIt.Model.Requests.Auth;
using JustDoIt.Model.Responses;
using JustDoIt.Model.Responses.Auth;

namespace JustDoIt.Service.Abstractions
{
    public interface IUserService// : IGenericService<UserResponse>
    {
        Task<RequestResponse<UserResponse>> GetCurrentUserData(string request);
        Task<RequestResponse<UserLoginResponse>> LoginAsync(UserLoginRequest request);
        Task<Result> LogoutAsync();
        Task<RequestResponse<UserRegistrationResponse>> RegisterAsync(UserRegistrationRequest request);
        Task<RequestResponse<string>> UpdateUser(UpdateUserRequest request);
    }
}
