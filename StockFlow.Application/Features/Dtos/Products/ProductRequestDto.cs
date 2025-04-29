namespace StockFlow.Application.Features.Dtos.Products
{
    public record ProductRequestDto(
        string Name,
        string? Description,
        decimal Price,
        int Stock,
        int CategoryId
    );
}
