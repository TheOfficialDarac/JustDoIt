using Microsoft.AspNetCore.Identity;

namespace JustDoIt.Model.Database
{
    public partial class ApplicationUser : IdentityUser
    {

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PictureUrl { get; set; }
        public virtual ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

        public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();

        public virtual ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
    }
}
