using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockFlow.Api.Domain.Entities;

public partial class SupplierEntity
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string? Contact { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [InverseProperty("Supplier")]
    public virtual ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}
