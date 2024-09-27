using JustDoIt.Model.Requests.Abstractions;

namespace JustDoIt.Model.Requests.Auth
{
    public class UserRegistrationRequest : CreateRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
