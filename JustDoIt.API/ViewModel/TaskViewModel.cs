namespace JustDoIt.API.ViewModel;
public class TaskViewModel
{
    public int? Id { get; set; }

    public string? Title { get; set; }

    public string? AdminId { get; set; }

    public string? Description { get; set; }

    public int ProjectId { get; set; }

    public string? PictureUrl { get; set; }

    public DateTime? Deadline { get; set; }

    public string? State { get; set; }
}