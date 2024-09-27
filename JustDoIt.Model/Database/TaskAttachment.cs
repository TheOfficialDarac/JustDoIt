using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class TaskAttachment
{
    public int Id { get; set; }

    public int? TaskId { get; set; }

    public string? Filepath { get; set; }

    public virtual Task? Task { get; set; }
}
