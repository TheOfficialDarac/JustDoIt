using JustDoIt.Model.Responses.Abstractions;

namespace JustDoIt.Model.Responses.Projects
{
    public class ProjectResponse : Response
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? PictureUrl { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
