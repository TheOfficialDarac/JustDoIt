using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class UserTask
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public int TaskId { get; set; }

    public DateTime AssignDate { get; set; }

    public bool IsActive { get; set; }

    public virtual Task Task { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;
}
