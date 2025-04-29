namespace StockFlow.Application.Features.Dtos.Customers
{
    public record CustomerResponseDto(
        int Id,
        string Name,
        string? Email,
        string? Phone,
        string? Address
        );
}
