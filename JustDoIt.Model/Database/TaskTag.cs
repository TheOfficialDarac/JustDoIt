using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class TaskTag
{
    public int Id { get; set; }

    public int TagId { get; set; }

    public int? TaskId { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public virtual Task? Task { get; set; }
}
