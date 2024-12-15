using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;

public partial class ChineseAuctionContext : DbContext
{
    public ChineseAuctionContext()
    {
    }

    public ChineseAuctionContext(DbContextOptions<ChineseAuctionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Donor> Donors { get; set; }

    public virtual DbSet<Gift> Gifts { get; set; }

    public virtual DbSet<Optional> Optionals { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=DESKTOP-P6FFS03;initial catalog=ChineseAuction;Integrated Security=SSPI;Persist Security Info=False;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BB84D2D89");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Donor>(entity =>
        {
            entity.HasKey(e => e.DonorId).HasName("PK__Donors__052E3F788362A914");

            entity.HasIndex(e => e.Email, "UQ__Donors__A9D105346A61C5DE").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<Gift>(entity =>
        {
            entity.HasKey(e => e.GiftId).HasName("PK__Gifts__4A40E60508CE312E");

            entity.Property(e => e.GiftName).HasMaxLength(100);
            entity.Property(e => e.ImageUrl).HasMaxLength(300);
            entity.Property(e => e.TicketCost).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Gifts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gifts__CategoryI__412EB0B6");

            entity.HasOne(d => d.Donor).WithMany(p => p.Gifts)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gifts__DonorId__4222D4EF");
        });

        modelBuilder.Entity<Optional>(entity =>
        {
            entity.HasKey(e => e.WinnerId).HasName("PK__Optional__8A3D1DA87AC29E96");

            entity.ToTable("Optional");

            entity.Property(e => e.WinnerEmail).HasMaxLength(100);
            entity.Property(e => e.WinnerName).HasMaxLength(100);
            entity.Property(e => e.WinnerPhone).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.WinningDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Gift).WithMany(p => p.Optionals)
                .HasForeignKey(d => d.GiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Optional__GiftId__48CFD27E");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__Purchase__6B0A6BBE95245DBB");

            entity.Property(e => e.BuyerEmail).HasMaxLength(100);
            entity.Property(e => e.BuyerName).HasMaxLength(100);
            entity.Property(e => e.BuyerPhone).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

            entity.HasOne(d => d.Gift).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.GiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Purchases__GiftI__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
