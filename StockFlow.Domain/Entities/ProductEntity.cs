using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockFlow.Api.Domain.Entities;

public partial class ProductEntity
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    public int Stock { get; set; }

    [StringLength(50)]
    public string? Category { get; set; }

    public int? SupplierId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; } = new List<OrderDetailEntity>();

    [InverseProperty("Product")]
    public virtual ICollection<StockHistoryEntity> StockHistories { get; set; } = new List<StockHistoryEntity>();

    [ForeignKey("SupplierId")]
    [InverseProperty("Products")]
    public virtual SupplierEntity? Supplier { get; set; }
}
