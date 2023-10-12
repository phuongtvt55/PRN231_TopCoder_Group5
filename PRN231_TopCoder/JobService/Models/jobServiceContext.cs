using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace JobService.Models
{
    public partial class jobServiceContext : DbContext
    {
        public jobServiceContext()
        {
        }

        public jobServiceContext(DbContextOptions<jobServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobCategory> JobCategories { get; set; }
        public virtual DbSet<JobRank> JobRanks { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=123456;database=jobService");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.ContractType).HasMaxLength(50);

                entity.Property(e => e.JobTitle).HasMaxLength(100);

                entity.Property(e => e.Nationality).HasMaxLength(50);

                entity.Property(e => e.PostDate).HasColumnType("datetime");

                entity.Property(e => e.RankId).HasColumnName("RankID");

                entity.Property(e => e.Salary).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Website).HasMaxLength(100);
            });

            modelBuilder.Entity<JobCategory>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.CategoryId })
                    .HasName("PK_JobCategory_1");

                entity.ToTable("JobCategory");

                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.JobCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobCategory_Category");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobCategories)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobCategory_Job");
            });

            modelBuilder.Entity<JobRank>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.RankId });

                entity.ToTable("JobRank");

                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.RankId).HasColumnName("RankID");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobRanks)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobRank_Job");

                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.JobRanks)
                    .HasForeignKey(d => d.RankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobRank_Rank");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("Rank");

                entity.Property(e => e.RankId).HasColumnName("RankID");

                entity.Property(e => e.RankName).HasMaxLength(50);
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.Property(e => e.WishlistId).HasColumnName("WishlistID");

                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_Wishlists_Job");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
