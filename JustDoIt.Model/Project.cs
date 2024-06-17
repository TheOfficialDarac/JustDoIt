using System;
using System.Collections.Generic;

namespace JustDoIt.Model;

public partial class Project
{
    public int Id { get; set; }

    public string? AdminId { get; set; }

    public string? Title { get; set; }

    public string? PictureUrl { get; set; }

    public string? Description { get; set; }

    public virtual AppUser? Admin { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
}
