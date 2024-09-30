using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JustDoIt.Model.Database;

namespace JustDoIt.DAL;

public partial class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationContext()
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<IssueAttachment> IssueAttachments { get; set; }

    public virtual DbSet<IssueComment> IssueComments { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectCategory> ProjectCategories { get; set; }

    public virtual DbSet<ProjectRole> ProjectRoles { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Model.Database.Task> Tasks { get; set; }

    public virtual DbSet<TaskAttachment> TaskAttachments { get; set; }

    public virtual DbSet<TaskComment> TaskComments { get; set; }

    public virtual DbSet<TaskTag> TaskTags { get; set; }

    public virtual DbSet<UserProject> UserProjects { get; set; }

    public virtual DbSet<UserTask> UserTasks { get; set; }


    //  is override is useless as same is set in Program.cs
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseSqlServer(Configuration[""]);
    //         // "Server=localhost;Database=task_manager;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //  added for identity
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.ToTable("ATTACHMENTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Filepath)
                .HasMaxLength(260)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("FILEPATH");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("CATEGORIES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("VALUE");
        });

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.ToTable("ISSUES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.IssuerId)
                .HasMaxLength(450)
                .HasColumnName("ISSUER_ID");
            entity.Property(e => e.ProjectId).HasColumnName("PROJECT_ID");
            entity.Property(e => e.SolvedDate)
                .HasColumnType("datetime")
                .HasColumnName("SOLVED_DATE");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.Issuer).WithMany(p => p.Issues)
                .HasForeignKey(d => d.IssuerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISSUES_USERS");

            entity.HasOne(d => d.Project).WithMany(p => p.Issues)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISSUES_PROJECTS");
        });

        modelBuilder.Entity<IssueAttachment>(entity =>
        {
            entity.ToTable("ISSUE_ATTACHMENTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AttachmentId).HasColumnName("ATTACHMENT_ID");
            entity.Property(e => e.IssueId).HasColumnName("ISSUE_ID");

            entity.HasOne(d => d.Attachment).WithMany(p => p.IssueAttachments)
                .HasForeignKey(d => d.AttachmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISSUE_ATTACHMENTS_ATTACHMENTS");

            entity.HasOne(d => d.Issue).WithMany(p => p.IssueAttachments)
                .HasForeignKey(d => d.IssueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISSUE_ATTACHMENTS_ISSUES");
        });

        modelBuilder.Entity<IssueComment>(entity =>
        {
            entity.ToTable("ISSUE_COMMENTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.IssueId).HasColumnName("ISSUE_ID");
            entity.Property(e => e.LastChangeDate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("LAST_CHANGE_DATE");
            entity.Property(e => e.Text)
                .HasColumnType("text")
                .HasColumnName("TEXT");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Issue).WithMany(p => p.IssueComments)
                .HasForeignKey(d => d.IssueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISSUE_COMMENTS_TASKS");

            entity.HasOne(d => d.User).WithMany(p => p.IssueComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISSUE_COMMENTS_USERS");
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.ToTable("PRIORITIES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Tag)
                .HasMaxLength(50)
                .HasColumnName("TAG");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .HasColumnName("VALUE");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("PROJECTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Key)
                .HasMaxLength(32)
                .HasColumnName("KEY");
            entity.Property(e => e.PictureUrl)
                .HasMaxLength(2083)
                .HasColumnName("PICTURE_URL");
            entity.Property(e => e.StatusId)
                .HasDefaultValue(1)
                .HasColumnName("STATUS_ID");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.Status).WithMany(p => p.Projects)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTS_STATUS");
        });

        modelBuilder.Entity<ProjectCategory>(entity =>
        {
            entity.ToTable("PROJECT_CATEGORIES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.ProjectId).HasColumnName("PROJECT_ID");

            entity.HasOne(d => d.Category).WithMany(p => p.ProjectCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECT_CATEGORIES_CATEGORY");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectCategories)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECT_CATEGORIES_PROJECTS");
        });

        modelBuilder.Entity<ProjectRole>(entity =>
        {
            entity.ToTable("PROJECT_ROLES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleDescription)
                .HasMaxLength(400)
                .HasColumnName("ROLE_DESCRIPTION");
            entity.Property(e => e.RoleName)
                .HasMaxLength(200)
                .HasColumnName("ROLE_NAME");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("STATES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Tag)
                .HasMaxLength(50)
                .HasColumnName("TAG");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .HasColumnName("VALUE");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("STATUS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Tag)
                .HasMaxLength(50)
                .HasColumnName("TAG");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .HasColumnName("VALUE");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("TAGS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .HasColumnName("VALUE");
        });

        modelBuilder.Entity<Model.Database.Task>(entity =>
        {
            entity.ToTable("TASKS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("DEADLINE");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.IssuerId)
                .HasMaxLength(450)
                .HasColumnName("ISSUER_ID");
            entity.Property(e => e.LastChangeDate)
                .HasColumnType("datetime")
                .HasColumnName("LAST_CHANGE_DATE");
            entity.Property(e => e.PictureUrl)
                .HasMaxLength(2083)
                .HasColumnName("PICTURE_URL");
            entity.Property(e => e.PriorityId)
                .HasDefaultValue(1)
                .HasColumnName("PRIORITY_ID");
            entity.Property(e => e.ProjectId).HasColumnName("PROJECT_ID");
            entity.Property(e => e.StateId)
                .HasDefaultValue(1)
                .HasColumnName("STATE_ID");
            entity.Property(e => e.StatusId)
                .HasDefaultValue(1)
                .HasColumnName("STATUS_ID");
            entity.Property(e => e.Summary)
                .HasMaxLength(120)
                .HasColumnName("SUMMARY");

            entity.HasOne(d => d.Issuer).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IssuerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASKS_USERS");

            entity.HasOne(d => d.Priority).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.PriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASKS_PRIORITIES");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_PROJECTS");

            entity.HasOne(d => d.State).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASKS_STATES");

            entity.HasOne(d => d.Status).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASKS_STATUS");
        });

        modelBuilder.Entity<TaskAttachment>(entity =>
        {
            entity.ToTable("TASK_ATTACHMENTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AttachmentId).HasColumnName("ATTACHMENT_ID");
            entity.Property(e => e.TaskId).HasColumnName("TASK_ID");

            entity.HasOne(d => d.Attachment).WithMany(p => p.TaskAttachments)
                .HasForeignKey(d => d.AttachmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_ATTACHMENTS_ATTACHMENTS");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskAttachments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_ATTACHMENTS_TASKS");
        });

        modelBuilder.Entity<TaskComment>(entity =>
        {
            entity.ToTable("TASK_COMMENTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.LastChangeDate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("LAST_CHANGE_DATE");
            entity.Property(e => e.TaskId).HasColumnName("TASK_ID");
            entity.Property(e => e.Text)
                .HasColumnType("text")
                .HasColumnName("TEXT");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskComments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_COMMENTS_TASKS");

            entity.HasOne(d => d.User).WithMany(p => p.TaskComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_COMMENTS_USERS");
        });

        modelBuilder.Entity<TaskTag>(entity =>
        {
            entity.ToTable("TASK_TAGS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TagId).HasColumnName("TAG_ID");
            entity.Property(e => e.TaskId).HasColumnName("TASK_ID");

            entity.HasOne(d => d.Tag).WithMany(p => p.TaskTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_TAGS_TAGS");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskTags)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_TASK_TAGS_TASKS");
        });

        modelBuilder.Entity<UserProject>(entity =>
        {
            entity.ToTable("USER_PROJECTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsFavorite).HasColumnName("IS_FAVORITE");
            entity.Property(e => e.IsVerified).HasColumnName("IS_VERIFIED");
            entity.Property(e => e.ProjectId).HasColumnName("PROJECT_ID");
            entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");
            entity.Property(e => e.Token)
                .HasMaxLength(37)
                .IsUnicode(false)
                .HasColumnName("TOKEN");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Project).WithMany(p => p.UserProjects)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_PROJECTS_PROJECTS");

            entity.HasOne(d => d.Role).WithMany(p => p.UserProjects)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_PROJECTS_PROJECT_ROLES");

            entity.HasOne(d => d.User).WithMany(p => p.UserProjects)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_PROJECTS_USERS");
        });

        modelBuilder.Entity<UserTask>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TaskId });

            entity.ToTable("USER_TASKS");

            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.TaskId).HasColumnName("TASK_ID");
            entity.Property(e => e.AssignDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ASSIGN_DATE");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");

            entity.HasOne(d => d.Task).WithMany(p => p.UserTasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_TASKS_TASKS");

            entity.HasOne(d => d.User).WithMany(p => p.UserTasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_TASKS_USERS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
