using System;
using System.Collections.Generic;

namespace JustDoIt.Model;

public partial class Project
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? PictureUrl { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
}
