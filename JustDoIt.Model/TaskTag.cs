using System;
using System.Collections.Generic;

namespace JustDoIt.Model;

public partial class TaskTag
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int? TaskId { get; set; }

    public virtual Task? Task { get; set; }
}
