using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class Project
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string? Description { get; set; }

    public string? PictureUrl { get; set; }

    public int StatusId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual ICollection<ProjectCategory> ProjectCategories { get; set; } = new List<ProjectCategory>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
}
