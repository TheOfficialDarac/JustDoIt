using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class ProjectCategory
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int ProjectId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
