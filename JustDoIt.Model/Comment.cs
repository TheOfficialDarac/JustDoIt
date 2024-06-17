using System;
using System.Collections.Generic;

namespace JustDoIt.Model;

public partial class Comment
{
    public string UserId { get; set; } = null!;

    public int Id { get; set; }

    public int TaskId { get; set; }

    public string Text { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;

    public virtual AppUser User { get; set; } = null!;
}
