using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class Status
{
    public int Id { get; set; }

    public string? Tag { get; set; }

    public string? Value { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
