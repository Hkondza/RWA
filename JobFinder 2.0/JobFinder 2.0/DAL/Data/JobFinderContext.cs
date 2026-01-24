using System;
using System.Collections.Generic;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public partial class JobFinderContext : DbContext
{
    public JobFinderContext()
    {
    }

    public JobFinderContext(DbContextOptions<JobFinderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Firm> Firms { get; set; }

    public virtual DbSet<JobApplication> JobApplications { get; set; }

    public virtual DbSet<JobOffer> JobOffers { get; set; }

    public virtual DbSet<JobType> JobTypes { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFirm> UserFirms { get; set; }

    public virtual DbSet<V1> V1s { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Firm>(entity =>
        {
            entity.HasKey(e => e.Idfirm).HasName("PK__Firm__42762D584C94F754");

            entity.ToTable("Firm");

            entity.HasIndex(e => e.FirmName, "UQ__Firm__129C392E664CE118").IsUnique();

            entity.Property(e => e.Idfirm).HasColumnName("IDFirm");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirmName).HasMaxLength(100);
            entity.Property(e => e.JobTypeId).HasColumnName("JobTypeID");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.HasOne(d => d.JobType).WithMany(p => p.Firms)
                .HasForeignKey(d => d.JobTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Firm_JobType");
        });

        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.HasKey(e => e.IdjobApplication).HasName("PK__JobAppli__E90D216D8E13D624");

            entity.ToTable("JobApplication");

            entity.Property(e => e.IdjobApplication).HasColumnName("IDJobApplication");
            entity.Property(e => e.AppliedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.JobOfferId).HasColumnName("JobOfferID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Applied");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.JobOffer).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.JobOfferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobApplication_JobOffer");

            entity.HasOne(d => d.User).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobApplication_User");
        });

        modelBuilder.Entity<JobOffer>(entity =>
        {
            entity.HasKey(e => e.IdjobOffer).HasName("PK__JobOffer__F4D201933FFFB21B");

            entity.ToTable("JobOffer");

            entity.Property(e => e.IdjobOffer).HasColumnName("IDJobOffer");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FirmId).HasColumnName("FirmID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.JobTypeId).HasColumnName("JobTypeID");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Salary).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Firm).WithMany(p => p.JobOffers)
                .HasForeignKey(d => d.FirmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobOffer_Firm");

            entity.HasOne(d => d.JobType).WithMany(p => p.JobOffers)
                .HasForeignKey(d => d.JobTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobOffer_JobType");

            entity.HasOne(d => d.Location).WithMany(p => p.JobOffers)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobOffer_Location");
        });

        modelBuilder.Entity<JobType>(entity =>
        {
            entity.HasKey(e => e.IdjobType).HasName("PK__JobType__40FBA956268D1C09");

            entity.ToTable("JobType");

            entity.HasIndex(e => e.JobName, "UQ__JobType__F1AC1A95E8FD3083").IsUnique();

            entity.Property(e => e.IdjobType).HasColumnName("IDJobType");
            entity.Property(e => e.JobName).HasMaxLength(100);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Idlocation).HasName("PK__Location__C2B75277805DCF75");

            entity.ToTable("Location");

            entity.HasIndex(e => e.LocationName, "UQ__Location__F946BB84D8855C4F").IsUnique();

            entity.Property(e => e.Idlocation).HasColumnName("IDLocation");
            entity.Property(e => e.LocationName).HasMaxLength(100);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Log__3214EC074BA2253B");

            entity.ToTable("Log");

            entity.Property(e => e.Level).HasMaxLength(50);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PK__Users__EAE6D9DF307CB7B3");

            entity.HasIndex(e => e.Email, "UK6dotkott2kjsp8vw4d0m25fb7").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4B8ECF4F8").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105342552A073").IsUnique();

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirmId).HasColumnName("FirmID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Firm).WithMany(p => p.Users)
                .HasForeignKey(d => d.FirmId)
                .HasConstraintName("FK_Users_Firm");
        });

        modelBuilder.Entity<UserFirm>(entity =>
        {
            entity.HasKey(e => e.IduserFirm).HasName("PK__UserFirm__090F5533CEA420FF");

            entity.ToTable("UserFirm");

            entity.Property(e => e.IduserFirm).HasColumnName("IDUserFirm");
            entity.Property(e => e.ApprovedAt).HasColumnType("datetime");
            entity.Property(e => e.FirmId).HasColumnName("FirmID");
            entity.Property(e => e.RequestedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Firm).WithMany(p => p.UserFirms)
                .HasForeignKey(d => d.FirmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserFirm_Firm");

            entity.HasOne(d => d.User).WithMany(p => p.UserFirms)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserFirm_User");
        });

        modelBuilder.Entity<V1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v1");

            entity.Property(e => e.Firma)
                .HasMaxLength(100)
                .HasColumnName("FIRMA");
            entity.Property(e => e.IdjobApplication).HasColumnName("IDJobApplication");
            entity.Property(e => e.Ime)
                .HasMaxLength(201)
                .HasColumnName("IME");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Idworker).HasName("PK__Workers__3B0FFE715A856AEE");

            entity.Property(e => e.Idworker).HasColumnName("IDWorker");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.JobApplication).WithMany(p => p.Workers)
                .HasForeignKey(d => d.JobApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workers_JobApplication");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
