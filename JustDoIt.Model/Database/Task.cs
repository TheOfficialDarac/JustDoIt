﻿using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class Task
{
    public int Id { get; set; }

    public string IssuerId { get; set; } = null!;

    public string? Summary { get; set; }

    public string? Description { get; set; }

    public int ProjectId { get; set; }

    public string? PictureUrl { get; set; }

    public DateTime? Deadline { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? LastChangeDate { get; set; }

    public int PriorityId { get; set; }

    public int StateId { get; set; }

    public int StatusId { get; set; }

    public virtual ApplicationUser Issuer { get; set; } = null!;

    public virtual Priority Priority { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual State State { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<TaskAttachment> TaskAttachments { get; set; } = new List<TaskAttachment>();

    public virtual ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();

    public virtual ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();

    public virtual ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
}
