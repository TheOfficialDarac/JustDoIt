using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class Tag
{
    public int Id { get; set; }

    public string Value { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
}
