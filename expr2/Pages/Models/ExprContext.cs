using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using expr2.Pages.Models;

namespace expr2.Pages.Models;

public partial class ExprContext : DbContext
{
    public ExprContext()
    {
    }

    public ExprContext(DbContextOptions<ExprContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<UsageRequest> UsageRequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data source=.;Database=expr;Trusted_Connection=True;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__727E83EBEEC58CBA");

            entity.ToTable("Item");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnName("ItemID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.Model).HasMaxLength(100);
            entity.Property(e => e.Origin).HasMaxLength(100);
            entity.Property(e => e.Specification).HasMaxLength(100);
            entity.Property(e => e.ItemImage).HasMaxLength(100);
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("PK__Stock__2C83A9E241B5280D");

            entity.ToTable("Stock");

            entity.Property(e => e.StockId)
                .ValueGeneratedNever()
                .HasColumnName("StockID");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Item).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_Stock_Item");
        });

        modelBuilder.Entity<UsageRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__UsageReq__33A8519ADB5D4E36");

            entity.ToTable("UsageRequest");

            entity.Property(e => e.RequestId)
                .ValueGeneratedNever()
                .HasColumnName("RequestID");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.RequestedBy).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Item).WithMany(p => p.UsageRequests)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_UsageRequest_Item");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
