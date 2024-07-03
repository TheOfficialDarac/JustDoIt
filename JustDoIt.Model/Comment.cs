using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JustDoIt.Model;

public partial class Comment
{
    public string? UserId { get; set; } = null!;

    public int Id { get; set; }

    public int? TaskId { get; set; }

    public string Text { get; set; } = null!;

    [JsonIgnore]
    public virtual Task? Task { get; set; } = null!;
    [JsonIgnore]
    public virtual AppUser? User { get; set; } = null!;
}
