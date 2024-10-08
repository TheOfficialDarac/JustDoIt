﻿using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class ProjectRole
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public string? RoleDescription { get; set; }

    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
}
