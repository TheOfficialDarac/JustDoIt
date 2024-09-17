using JustDoIt.Model.DTOs.Responses.Abstractions;

namespace JustDoIt.Model.DTOs.Requests.Abstractions
{
    public class GetSingleUserRequest : Response
    {
        public string Id { get; set; } = string.Empty;
    }
}
