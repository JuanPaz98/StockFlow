namespace StockFlow.Application.Features.Dtos.Orders
{
    public record OrderDetailsDto(
        int ProductId,
        int Quantity,
        decimal UnitPrice
        );
}
