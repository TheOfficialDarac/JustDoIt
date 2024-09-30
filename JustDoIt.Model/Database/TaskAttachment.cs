using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class TaskAttachment
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    public int AttachmentId { get; set; }

    public virtual Attachment Attachment { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;
}
