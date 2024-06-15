namespace JustDoIt.Model;

public partial class Task
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public int? AdminId { get; set; }

    public string Description { get; set; } = string.Empty;

    public int? ProjectId { get; set; }

    public string PictureUrl { get; set; } = string.Empty;

    public DateTime? Deadline { get; set; }

    public string State { get; set; } = string.Empty;

    public virtual AppUser? Admin { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();

    public virtual Project? Project { get; set; }

    public virtual ICollection<AppUser> Users { get; set; } = new List<AppUser>();
}
