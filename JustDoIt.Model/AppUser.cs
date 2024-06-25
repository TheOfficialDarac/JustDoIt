using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace JustDoIt.Model;
public partial class AppUser : IdentityUser
{

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PictureUrl { get; set; }

    [JsonIgnore]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [JsonIgnore]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [JsonIgnore]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    [JsonIgnore]

    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();

    [JsonIgnore]
    public virtual ICollection<Task> TasksNavigation { get; set; } = new List<Task>();
}
