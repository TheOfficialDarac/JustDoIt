using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class IssueAttachment
{
    public int Id { get; set; }

    public int IssueId { get; set; }

    public int AttachmentId { get; set; }

    public virtual Attachment Attachment { get; set; } = null!;

    public virtual Issue Issue { get; set; } = null!;
}
