namespace JustDoIt.Model.DTOs.Requests.Auth
{
    public class UserRegistrationRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
