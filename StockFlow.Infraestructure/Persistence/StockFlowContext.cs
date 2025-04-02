using Microsoft.EntityFrameworkCore;
using StockFlow.Api.Domain.Entities;
using StockFlow.Domain.Entities;

namespace StockFlow.Api.Infrastructure.Persistence;

public partial class StockFlowContext : DbContext
{
    public StockFlowContext()
    {
    }

    public StockFlowContext(DbContextOptions<StockFlowContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerEntity> Customers { get; set; }

    public virtual DbSet<OrderEntity> Orders { get; set; }

    public virtual DbSet<OrderDetailEntity> OrderDetails { get; set; }

    public virtual DbSet<ProductEntity> Products { get; set; }

    public virtual DbSet<StockHistoryEntity> StockHistories { get; set; }

    public virtual DbSet<SupplierEntity> Suppliers { get; set; }

    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<PaymentEntity> Payments { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07B9F7D302");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<OrderEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07968CCBA2");

            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue("Pending");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Customers");
            
            entity.HasMany(o => o.OrderDetails)
                .WithOne(d => d.Order)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderDetailEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC07A53BB79E");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__4BAC3F29");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Produ__4CA06362");
        });

        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC077C1922AB");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products).HasConstraintName("FK__Products__Suppli__4316F928");
        });

        modelBuilder.Entity<StockHistoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StockHis__3214EC07CC8E04D1");

            entity.Property(e => e.ChangeDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Product).WithMany(p => p.StockHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StockHist__Produ__52593CB8");
        });

        modelBuilder.Entity<SupplierEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC07AABF1916");
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0755B8DA37");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Role).HasDefaultValue("User");
        });

        modelBuilder.Entity<PaymentEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Payments");

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();

            entity.Property(e => e.AmountPaid)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            entity.HasOne(e => e.Order)
                .WithMany(p => p.Payments)
                .HasForeignKey(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
