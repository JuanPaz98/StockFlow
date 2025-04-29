namespace StockFlow.Application.Features.Dtos.Orders
{
    public record OrderWithIdDto(
        int Id,
        int CustomerId,
        DateTime? OrderDate,
        string Status,
        decimal TotalAmount,
        decimal PaidAmount,
        IEnumerable<OrderDetailsIdDto> OrderDetails
    );
}
