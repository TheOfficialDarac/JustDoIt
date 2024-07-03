using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JustDoIt.Model;

public partial class Label
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? TaskId { get; set; }
    [JsonIgnore]
    public virtual Task? Task { get; set; }
}
