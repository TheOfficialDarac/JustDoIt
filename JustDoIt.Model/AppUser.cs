using Microsoft.AspNetCore.Identity;

namespace JustDoIt.Model;
public partial class AppUser : IdentityUser
{

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PictureUrl { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();

    public virtual ICollection<Task> TasksNavigation { get; set; } = new List<Task>();
}
