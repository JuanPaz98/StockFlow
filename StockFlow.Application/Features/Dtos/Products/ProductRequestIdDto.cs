namespace StockFlow.Application.Features.Dtos.Products
{
    public record ProductRequestIdDto(
        int Id,
        string Name,
        string? Description,
        decimal Price,
        int Stock,
        int CategoryId
    );
}
