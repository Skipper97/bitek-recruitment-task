using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RecruitmentTask.Data.Entities;

namespace RecruitmentTask.Data.DbContexts;

public partial class RecruitmentTaskDbContext : DbContext
{
    public RecruitmentTaskDbContext()
    {
    }

    public RecruitmentTaskDbContext(DbContextOptions<RecruitmentTaskDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:RecruitmentTaskDatabaseConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("Inventory");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Manufacturer).HasMaxLength(300);
            entity.Property(e => e.Quantity).HasColumnType("decimal(14, 3)");
            entity.Property(e => e.ShippingCost).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.ShippingTime).HasMaxLength(50);
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.InternalId).HasName("PK__tmp_ms_x__56FE95DE6BCC4D75");

            entity.Property(e => e.InternalId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NetDiscountPrice).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.NetDiscountPricePerUnit).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.NetPrice).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Category).HasMaxLength(300);
            entity.Property(e => e.DefaultImage).HasMaxLength(300);
            entity.Property(e => e.Ean)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(300);
            entity.Property(e => e.ProducerName).HasMaxLength(300);
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
