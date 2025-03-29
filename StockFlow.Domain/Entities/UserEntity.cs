using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StockFlow.Api.Domain.Entities;

[Index("Username", Name = "UQ__Users__536C85E4CE369B88", IsUnique = true)]
[Index("Email", Name = "UQ__Users__A9D105343F6DCDF3", IsUnique = true)]
public partial class UserEntity
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(20)]
    public string Role { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }
}
