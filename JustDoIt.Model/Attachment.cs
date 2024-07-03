using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JustDoIt.Model;

public partial class Attachment
{
    public int Id { get; set; }

    public string Filepath { get; set; } = null!;

    public int? ProjectId { get; set; }

    public int? TaskId { get; set; }

    [JsonIgnore]
    public virtual Project? Project { get; set; }
    [JsonIgnore]
    public virtual Task? Task { get; set; }
}
