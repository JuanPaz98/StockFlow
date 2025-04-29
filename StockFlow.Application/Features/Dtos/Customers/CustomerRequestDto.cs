
namespace StockFlow.Application.Features.Dtos.Customers
{
    public record CustomerRequestDto( 
        string Name,
        string? Email, 
        string? Phone,
        string? Address
        );
}
