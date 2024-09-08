using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Model.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? PictureUrl { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
