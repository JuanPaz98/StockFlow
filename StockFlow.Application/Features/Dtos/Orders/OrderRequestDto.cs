namespace StockFlow.Application.Features.Dtos.Orders
{
    public record OrderRequestDto(
        int CustomerId,
        DateTime? OrderDate,
        string Status,
        decimal TotalAmount,
        decimal PaidAmount,
        IEnumerable<OrderDetailsDto> OrderDetails
    );
}
