using JustDoIt.Common;
using JustDoIt.Model.DTOs.Requests.Auth;
using JustDoIt.Model.DTOs.Responses;
using JustDoIt.Service.Abstractions.Common;

namespace JustDoIt.Service.Abstractions
{
    public interface IUserService //: IGenericService
    {
        Task<RequestResponse<string>> LoginAsync(UserLoginRequest request);
        Task<Result> LogoutAsync();
        //Task<Result> RegisterAsync(UserRegistrationRequest data);
    }
}
