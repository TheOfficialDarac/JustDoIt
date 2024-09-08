﻿using System;
using System.Collections.Generic;

namespace JustDoIt.Model;

public partial class UserProject
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public int ProjectId { get; set; }

    public bool IsVerified { get; set; }

    public string? Token { get; set; }

    public int RoleId { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual ProjectRole Role { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;

    public virtual ICollection<ProjectClaim> Claims { get; set; } = new List<ProjectClaim>();
}
