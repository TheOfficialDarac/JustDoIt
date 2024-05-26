using JustDoIt.Model;
using Microsoft.EntityFrameworkCore;
using JustDoIt.Model;

namespace JustDoIt.DAL
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }

        public virtual DbSet<Attachment> Attachments { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Label> Labels { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Model.Task> Tasks { get; set; }

        public virtual DbSet<UserProject> UserProjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(optionsBuilder.con)
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=task_manager; Integrated Security=True; Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__app_user__3214EC271BFE4662");

                entity.ToTable("app_user");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("firstName");
                entity.Property(e => e.IsVerified).HasColumnName("isVerified");
                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastName");
                entity.Property(e => e.Password)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("password");
                entity.Property(e => e.PictureUrl)
                    .HasMaxLength(700)
                    .IsUnicode(false)
                    .HasColumnName("pictureURL");
                entity.Property(e => e.Token)
                    .HasMaxLength(37)
                    .IsUnicode(false)
                    .HasColumnName("token");
                entity.Property(e => e.Username)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasMany(d => d.TasksNavigation).WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserTask",
                        r => r.HasOne<Model.Task>().WithMany()
                            .HasForeignKey("TaskId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_UserTaskTask"),
                        l => l.HasOne<AppUser>().WithMany()
                            .HasForeignKey("UserId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_UserTaskUser"),
                        j =>
                        {
                            j.HasKey("UserId", "TaskId").HasName("PK__user_tas__F64FC985713DC9B6");
                            j.ToTable("user_task");
                            j.IndexerProperty<int>("UserId").HasColumnName("userID");
                            j.IndexerProperty<int>("TaskId").HasColumnName("taskID");
                        });
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__attachme__3214EC271006E8BF");

                entity.ToTable("attachment");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Filepath)
                    .HasColumnType("text")
                    .HasColumnName("filepath");
                entity.Property(e => e.ProjectId).HasColumnName("projectID");
                entity.Property(e => e.TaskId).HasColumnName("taskID");

                entity.HasOne(d => d.Project).WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_AttachmentProject");

                entity.HasOne(d => d.Task).WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_AttachmentTask");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__comment__3214EC27955DCE71");

                entity.ToTable("comment");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.TaskId).HasColumnName("taskID");
                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasColumnName("text");
                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Task).WithMany(p => p.Comments)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_CommentTask");

                entity.HasOne(d => d.User).WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_CommentUser");
            });

            modelBuilder.Entity<Label>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__label__3214EC279F081C1E");

                entity.ToTable("label");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");
                entity.Property(e => e.TaskId).HasColumnName("taskID");
                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Task).WithMany(p => p.Labels)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_LabelTask");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__project__3214EC2768092F7F");

                entity.ToTable("project");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.AdminId).HasColumnName("adminID");
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("description");
                entity.Property(e => e.PictureUrl)
                    .HasMaxLength(700)
                    .IsUnicode(false)
                    .HasColumnName("pictureURL");
                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Admin).WithMany(p => p.Projects)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK__project__adminID__4BAC3F29");
            });

            modelBuilder.Entity<Model.Task>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__task__3214EC27CBF75982");

                entity.ToTable("task");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.AdminId).HasColumnName("adminID");
                entity.Property(e => e.Deadline).HasColumnName("deadline");
                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");
                entity.Property(e => e.PictureUrl)
                    .HasMaxLength(700)
                    .IsUnicode(false)
                    .HasColumnName("pictureURL");
                entity.Property(e => e.ProjectId).HasColumnName("projectID");
                entity.Property(e => e.State)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("state");
                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Admin).WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK_UserTask");

                entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_TaskProject");
            });

            modelBuilder.Entity<UserProject>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProjectId }).HasName("PK__user_pro__8A850807BB532CAA");

                entity.ToTable("user_project");

                entity.Property(e => e.UserId).HasColumnName("userID");
                entity.Property(e => e.ProjectId).HasColumnName("projectID");
                entity.Property(e => e.IsVerified).HasColumnName("isVerified");
                entity.Property(e => e.ProjectRole)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("project_role");
                entity.Property(e => e.Token)
                    .HasMaxLength(37)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.HasOne(d => d.Project).WithMany(p => p.UserProjects)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProjectProject");

                entity.HasOne(d => d.User).WithMany(p => p.UserProjects)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProjectUser");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
