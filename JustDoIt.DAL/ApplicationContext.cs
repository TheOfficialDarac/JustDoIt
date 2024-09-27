using System;
using System.Collections.Generic;
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

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectClaim> ProjectClaims { get; set; }

    public virtual DbSet<ProjectRole> ProjectRoles { get; set; }

    public virtual DbSet<RoleClaim> RoleClaims { get; set; }

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
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.PictureUrl)
                .HasMaxLength(2083)
                .HasColumnName("PICTURE_URL");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<ProjectClaim>(entity =>
        {
            entity.ToTable("PROJECT_CLAIMS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Claim)
                .HasMaxLength(300)
                .HasColumnName("CLAIM");
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

        modelBuilder.Entity<RoleClaim>(entity =>
        {
            entity.ToTable("ROLE_CLAIMS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClaimId).HasColumnName("CLAIM_ID");
            entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

            entity.HasOne(d => d.Claim).WithMany(p => p.RoleClaims)
                .HasForeignKey(d => d.ClaimId)
                .HasConstraintName("FK__ROLE_CLAI__CLAIM__47A6A41B");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleClaims)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROLE_CLAIMS_PROJECT_ROLES");
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
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IssuerId)
                .HasMaxLength(450)
                .HasColumnName("ISSUER_ID");
            entity.Property(e => e.PictureUrl)
                .HasMaxLength(2083)
                .HasColumnName("PICTURE_URL");
            entity.Property(e => e.ProjectId).HasColumnName("PROJECT_ID");
            entity.Property(e => e.State)
                .HasMaxLength(4)
                .HasColumnName("STATE");
            entity.Property(e => e.Summary)
                .HasMaxLength(120)
                .HasColumnName("SUMMARY");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.Issuer).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IssuerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASKS_USERS");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_PROJECTS");
        });

        modelBuilder.Entity<TaskAttachment>(entity =>
        {
            entity.ToTable("TASK_ATTACHMENTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Filepath)
                .HasMaxLength(260)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("FILEPATH");
            entity.Property(e => e.TaskId).HasColumnName("TASK_ID");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskAttachments)
                .HasForeignKey(d => d.TaskId)
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
            entity.Property(e => e.TaskId).HasColumnName("TASK_ID");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskTags)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_TASK_TAGS_TASKS");
        });

        modelBuilder.Entity<UserProject>(entity =>
        {
            entity.ToTable("USER_PROJECTS");

            entity.Property(e => e.Id).HasColumnName("ID");
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

            entity.HasMany(d => d.Claims).WithMany(p => p.UserProjects)
                .UsingEntity<Dictionary<string, object>>(
                    "ProjectUserClaim",
                    r => r.HasOne<ProjectClaim>().WithMany()
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PROJECT_USER_CLAIMS_PROJECT_CLAIMS"),
                    l => l.HasOne<UserProject>().WithMany()
                        .HasForeignKey("UserProjectsId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PROJECT_USER_CLAIMS_USER_PROJECTS"),
                    j =>
                    {
                        j.HasKey("UserProjectsId", "ClaimId");
                        j.ToTable("PROJECT_USER_CLAIMS");
                        j.IndexerProperty<int>("UserProjectsId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("USER_PROJECTS_ID");
                        j.IndexerProperty<int>("ClaimId").HasColumnName("CLAIM_ID");
                    });
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
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");

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
