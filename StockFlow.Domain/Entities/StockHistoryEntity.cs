using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockFlow.Api.Domain.Entities;

[Table("StockHistory")]
public partial class StockHistoryEntity
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    [StringLength(10)]
    public string ChangeType { get; set; } = null!;

    public int Quantity { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ChangeDate { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("StockHistories")]
    public virtual ProductEntity Product { get; set; } = null!;
}
