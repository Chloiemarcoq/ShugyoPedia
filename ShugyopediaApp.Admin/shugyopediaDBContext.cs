using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ShugyopediaApp.Admin.Models;

namespace ShugyopediaApp.Admin
{
    public partial class shugyopediaDBContext : DbContext
    {
        public shugyopediaDBContext()
        {
        }

        public shugyopediaDBContext(DbContextOptions<shugyopediaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<TopicResource> TopicResources { get; set; }
        public virtual DbSet<Training> Trainings { get; set; }
        public virtual DbSet<TrainingCategory> TrainingCategories { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Addr=LAPTOP-9T7BS5AI; database=shugyopediaDB; Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.RaterEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RaterName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RatingReview)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.TrainingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrainingIdRatings");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasIndex(e => e.TopicName, "UQ__Topics__6C795E8C95411B4B")
                    .IsUnique();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.TopicName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.Topics)
                    .HasForeignKey(d => d.TrainingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrainingID");
            });

            modelBuilder.Entity<TopicResource>(entity =>
            {
                entity.HasKey(e => e.ResourceId)
                    .HasName("PK__TopicRes__4ED1816F53C6B3BD");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.ResourceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ResourcePath)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.ResourceType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.TopicResources)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TopicID");
            });

            modelBuilder.Entity<Training>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.TrainingDescription)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.TrainingImage)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.training)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryID");
            });

            modelBuilder.Entity<TrainingCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__Training__19093A2BEA26C21D");

                entity.HasIndex(e => e.CategoryName, "UQ__Training__8517B2E0B83BA59A")
                    .IsUnique();

                entity.Property(e => e.CategoryIcon)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('<i class=\"fa-solid fa-notes-medical\"></i>')");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserId, "UQ__Users__1788CCAD6FD26CEC")
                    .IsUnique();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
