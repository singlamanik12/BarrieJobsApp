using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BarrieJobsApp.Models
{
    public partial class BarrieJobsContext : DbContext
    {
        public BarrieJobsContext()
        {
        }

        public BarrieJobsContext(DbContextOptions<BarrieJobsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Applicants> Applicants { get; set; }
        public virtual DbSet<JobPostings> JobPostings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
  //              optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=BarrieJobs;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.CartId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Applicants>(entity =>
            {
                entity.Property(e => e.ApplicantId).ValueGeneratedNever();

                entity.Property(e => e.ApplicantLocation).IsUnicode(false);

                entity.Property(e => e.ApplicantResume).IsUnicode(false);

                entity.HasOne(d => d.JobAppliedNavigation)
                    .WithMany(p => p.Applicants)
                    .HasForeignKey(d => d.JobApplied)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicants_Job_Postings");
            });

            modelBuilder.Entity<JobPostings>(entity =>
            {
                entity.Property(e => e.Company).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
