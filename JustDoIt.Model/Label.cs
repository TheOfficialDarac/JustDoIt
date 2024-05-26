namespace JustDoIt.Model;

public partial class Label
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? TaskId { get; set; }

    public virtual Task? Task { get; set; }
}
