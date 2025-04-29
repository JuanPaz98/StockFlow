

namespace StockFlow.Application.Features.Dtos.Customers
{
    public record CustomerRequestIdDto(
        int Id,
        string? Name,
        string? Email,
        string? Phone,
        string? Address);
}
