using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JustDoIt.Model;

public partial class Project
{
    public int Id { get; set; }

    public string? AdminId { get; set; }

    public string? Title { get; set; }

    public string? PictureUrl { get; set; }

    public string? Description { get; set; }

    public virtual AppUser? Admin { get; set; }
    [JsonIgnore]

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    [JsonIgnore]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    [JsonIgnore]
    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
}
