using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JustDoIt.Model;

namespace JustDoIt.DAL;

public partial class DataContext : IdentityDbContext<AppUser>
{
    public DataContext()
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
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
        => optionsBuilder.UseSqlServer(this.Database.GetDbConnection());
            // "Server=localhost;Database=task_manager;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__attachment");

            entity.ToTable("attachments");

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
            entity.HasKey(e => e.Id).HasName("PK_Comment");

            entity.ToTable("comments");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TaskId).HasColumnName("taskID");
            entity.Property(e => e.Text)
                .HasColumnType("text")
                .HasColumnName("text");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("userID");

            entity.HasOne(d => d.Task).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommentTask");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CommentUser");
        });

        modelBuilder.Entity<Label>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__label");

            entity.ToTable("labels");

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
            entity.HasKey(e => e.Id).HasName("PK_Project");

            entity.ToTable("project");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AdminId)
                .HasMaxLength(450)
                .HasColumnName("adminID");
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
                .HasConstraintName("FK__project__adminID");
        });

        modelBuilder.Entity<Model.Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__task");

            entity.ToTable("task");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AdminId)
                .HasMaxLength(450)
                .HasColumnName("adminID");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskProject");
        });

        modelBuilder.Entity<UserProject>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProjectId }).HasName("PK__user_project");

            entity.ToTable("user_project");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.ProjectId).HasColumnName("projectID");
            entity.Property(e => e.IsVerified).HasColumnName("isVerified");
            entity.Property(e => e.ProjectRole)
                .HasMaxLength(50)
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
