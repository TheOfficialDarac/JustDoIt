using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class Attachment
{
    public int Id { get; set; }

    public string? Filepath { get; set; }

    public virtual ICollection<IssueAttachment> IssueAttachments { get; set; } = new List<IssueAttachment>();

    public virtual ICollection<TaskAttachment> TaskAttachments { get; set; } = new List<TaskAttachment>();
}
