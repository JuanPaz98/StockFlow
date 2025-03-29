using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockFlow.Api.Domain.Entities;

public partial class OrderEntity
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OrderDate { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Total { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual CustomerEntity Customer { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; } = new List<OrderDetailEntity>();
}
