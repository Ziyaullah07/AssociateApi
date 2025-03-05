using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssociateApi.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAssociate> TblAssociates { get; set; }

    public virtual DbSet<TblContactInfo> TblContactInfos { get; set; }

    public virtual DbSet<TblLocation> TblLocations { get; set; }

    public virtual DbSet<TblOccupation> TblOccupations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-D0R4ICH\\SA;Initial Catalog=CodeAssesment;Persist Security Info=True;User ID=sa;Password=sa1234;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAssociate>(entity =>
        {
            entity.HasKey(e => e.AssociateId).HasName("PK__tblAssoc__AC40220FF821604C");

            entity.ToTable("tblAssociates");

            entity.Property(e => e.AssociateId).HasColumnName("AssociateID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.OccupationId).HasColumnName("OccupationID");

            entity.HasOne(d => d.Location).WithMany(p => p.TblAssociates)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__tblAssoci__Locat__5812160E");

            entity.HasOne(d => d.Occupation).WithMany(p => p.TblAssociates)
                .HasForeignKey(d => d.OccupationId)
                .HasConstraintName("FK__tblAssoci__Occup__59063A47");
        });

        modelBuilder.Entity<TblContactInfo>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__tblConta__5C6625BB33788226");

            entity.ToTable("tblContactInfo");

            entity.HasIndex(e => e.Email, "UQ__tblConta__A9D105346E4A4B12").IsUnique();

            entity.HasIndex(e => e.AssociateId, "UQ__tblConta__AC40220EC1BD2F89").IsUnique();

            entity.Property(e => e.ContactId).HasColumnName("ContactID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.AssociateId).HasColumnName("AssociateID");
            entity.Property(e => e.ContactNo).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(100);

            entity.HasOne(d => d.Associate).WithOne(p => p.TblContactInfo)
                .HasForeignKey<TblContactInfo>(d => d.AssociateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tblContac__Assoc__5DCAEF64");
        });

        modelBuilder.Entity<TblLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__tblLocat__E7FEA477C27FD1F8");

            entity.ToTable("tblLocations");

            entity.HasIndex(e => new { e.City, e.State }, "UQ__tblLocat__756CA3B7C541EF96").IsUnique();

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(50);
        });

        modelBuilder.Entity<TblOccupation>(entity =>
        {
            entity.HasKey(e => e.OccupationId).HasName("PK__tblOccup__8917118DDFE9253D");

            entity.ToTable("tblOccupations");

            entity.HasIndex(e => new { e.Occupation, e.Department }, "UQ__tblOccup__31392015F9F6F4DC").IsUnique();

            entity.Property(e => e.OccupationId).HasColumnName("OccupationID");
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.Occupation).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
