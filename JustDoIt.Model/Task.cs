﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JustDoIt.Model;

public partial class Task
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? AdminId { get; set; }

    public string? Description { get; set; }

    public int ProjectId { get; set; }

    public string? PictureUrl { get; set; }

    public DateTime? Deadline { get; set; }

    public string? State { get; set; }

    [JsonIgnore]
    public virtual AppUser? Admin { get; set; }

    [JsonIgnore]
    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    [JsonIgnore]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [JsonIgnore]

    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();
    [JsonIgnore]

    public virtual Project Project { get; set; } = null!;

    [JsonIgnore]

    public virtual ICollection<AppUser> Users { get; set; } = new List<AppUser>();
}
