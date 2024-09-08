using System;
using System.Collections.Generic;

namespace JustDoIt.Model;

public partial class RoleClaim
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int? ClaimId { get; set; }

    public virtual ProjectClaim? Claim { get; set; }

    public virtual ProjectRole Role { get; set; } = null!;
}
