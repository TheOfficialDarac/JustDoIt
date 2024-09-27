using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class TaskComment
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public string UserId { get; set; } = null!;

    public int TaskId { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? LastChangeDate { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;
}
