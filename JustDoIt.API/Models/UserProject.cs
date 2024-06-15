namespace JustDoIt.Model;

public partial class UserProject
{
    public int UserId { get; set; }

    public int ProjectId { get; set; }

    public bool? IsVerified { get; set; }

    public string Token { get; set; } = null!;

    public string ProjectRole { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual AppUser User { get; set; } = null!;
}
