using System;
using System.Collections.Generic;

namespace JustDoIt.Model.Database;

public partial class Issue
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? SolvedDate { get; set; }

    public string IssuerId { get; set; } = null!;

    public int ProjectId { get; set; }

    public virtual ICollection<IssueAttachment> IssueAttachments { get; set; } = new List<IssueAttachment>();

    public virtual ICollection<IssueComment> IssueComments { get; set; } = new List<IssueComment>();

    public virtual ApplicationUser Issuer { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
