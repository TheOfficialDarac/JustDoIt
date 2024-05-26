namespace JustDoIt.Model;

public partial class Attachment
{
    public int Id { get; set; }

    public string Filepath { get; set; } = null!;

    public int? ProjectId { get; set; }

    public int? TaskId { get; set; }

    public virtual Project? Project { get; set; }

    public virtual Task? Task { get; set; }
}
