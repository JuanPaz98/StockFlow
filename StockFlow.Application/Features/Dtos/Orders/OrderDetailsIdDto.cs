namespace StockFlow.Application.Features.Dtos.Orders
{
    public record OrderDetailsIdDto(
        int Id,
        int OrderId,
        int ProductId,
        int Quantity,
        decimal UnitPrice
    );
}
