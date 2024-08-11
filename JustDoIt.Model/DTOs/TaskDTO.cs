using System.ComponentModel.DataAnnotations;

namespace JustDoIt.Model.DTOs;
public class TaskDTO
{
    public int? Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? AdminId { get; set; }

    public string? Description { get; set; } = string.Empty;

    [Required]
    public int ProjectId { get; set; }

    public string? PictureUrl { get; set; }

    public DateTime? Deadline { get; set; }

    [Required]
    public string? State { get; set; }
}