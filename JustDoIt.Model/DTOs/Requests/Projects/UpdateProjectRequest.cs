using JustDoIt.Model.DTOs.Requests.Abstractions;
using System.Security;

namespace JustDoIt.Model.DTOs.Requests.Projects
{
    public class UpdateProjectRequest : UpdateRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string PictureUrl { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
