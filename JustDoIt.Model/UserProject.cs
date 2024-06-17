using System;
using System.Collections.Generic;

namespace JustDoIt.Model;

public partial class UserProject
{
    public string UserId { get; set; } = null!;

    public int ProjectId { get; set; }

    public bool? IsVerified { get; set; }

    public string? Token { get; set; }

    public string? ProjectRole { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual AppUser User { get; set; } = null!;
}
