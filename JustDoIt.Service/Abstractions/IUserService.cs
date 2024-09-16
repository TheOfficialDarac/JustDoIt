using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests.Auth;
using JustDoIt.Service.Abstractions.Common;

namespace JustDoIt.Service.Abstractions
{
    public interface IUserService// : IGenericService<ApplicationUserDTO>
    {
        Task<(string data, Result result)> LoginAsync(UserLoginRequest request);
        Task<Result> LogoutAsync();
        Task<Result> RegisterAsync(UserRegistrationRequest data);
    }
}
