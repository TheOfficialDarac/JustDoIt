using JustDoIt.Model.Responses.Abstractions;

namespace JustDoIt.Model.Requests.Abstractions
{
    public class GetSingleUserRequest : Response
    {
        public string Id { get; set; } = string.Empty;
    }
}
