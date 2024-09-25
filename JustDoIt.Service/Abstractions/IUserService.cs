﻿using JustDoIt.Common;
using JustDoIt.Model.DTOs;
using JustDoIt.Model.DTOs.Requests.Auth;
using JustDoIt.Model.DTOs.Responses;
using JustDoIt.Model.DTOs.Responses.Auth;
using JustDoIt.Service.Abstractions.Common;

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