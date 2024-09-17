using JustDoIt.Model.DTOs.Requests.Abstractions;

namespace JustDoIt.Model.DTOs.Requests.Auth
{
    public class UserRegistrationRequest:CreateRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
