namespace JustDoIt.Model;
public partial class AppUser
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public bool IsVerified { get; set; }

    public string Token { get; set; } = null!;

    public string? PictureUrl { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();

    public virtual ICollection<Task> TasksNavigation { get; set; } = new List<Task>();
}
