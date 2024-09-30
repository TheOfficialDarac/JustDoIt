using JustDoIt.Model.Requests.Abstractions;
using Microsoft.AspNetCore.Http;

namespace JustDoIt.Model.Requests.Projects
{
    public class CreateProjectRequest : CreateRequest
    {
        public string IssuerId = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Key { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public  int StatusId { get; set; } = 1;

        public IFormFile? Picture { get; set; } = null;

        public bool IsFavorite { get; set; } = false;

        //public IEnumerable<int> Categories { get; set; } = [];

    }
}
