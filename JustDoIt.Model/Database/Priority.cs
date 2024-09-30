using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class Priority
{
    public int Id { get; set; }

    public string? Tag { get; set; }

    public string? Value { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
