using System;
using System.Collections.Generic;

namespace JustDoIt.Model;

public partial class ProjectClaim
{
    public int Id { get; set; }

    public string? Claim { get; set; }

    public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();

    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
}
