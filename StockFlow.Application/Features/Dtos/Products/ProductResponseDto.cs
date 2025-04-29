namespace StockFlow.Application.Features.Dtos.Products
{
    public record ProductResponseDto(
        int Id,
        string Name,
        string Description,
        decimal Price,
        int Stock
        );
}
