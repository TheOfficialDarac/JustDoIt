using JustDoIt.Model.Responses.Abstractions;

namespace JustDoIt.Model.Responses.Projects
{
    public class ProjectResponse : Response
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Key { get; set; }
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
        public int? StatusId { get; set; }
        //public IEnumerable<int>? Categories { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
