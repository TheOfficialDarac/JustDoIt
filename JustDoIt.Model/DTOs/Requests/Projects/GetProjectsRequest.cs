using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Model.DTOs.Requests.Projects
{
    public class GetProjectsRequest
    {
        //public int? Id { get; set; }

        public string? Title { get; set; }

        //public string? Description { get; set; }

        //public string? PictureUrl { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? MinCreatedDate { get; set; }
        public DateTime? MaxCreatedDate { get; set; }
    }
}
