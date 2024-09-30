using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class Category
{
    public int Id { get; set; }

    public string Value { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<ProjectCategory> ProjectCategories { get; set; } = new List<ProjectCategory>();
}
