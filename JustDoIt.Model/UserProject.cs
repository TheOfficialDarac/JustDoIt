using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JustDoIt.Model;

public partial class UserProject
{
    public string UserId { get; set; } = null!;

    public int ProjectId { get; set; }

    public bool? IsVerified { get; set; }

    public string? Token { get; set; }

    public string? ProjectRole { get; set; }

    [JsonIgnore]
    public virtual Project Project { get; set; } = null!;

    [JsonIgnore]
    public virtual AppUser User { get; set; } = null!;
}
