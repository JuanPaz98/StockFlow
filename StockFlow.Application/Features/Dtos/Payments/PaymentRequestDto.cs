namespace StockFlow.Application.Features.Dtos.Payments
{
    public record PaymentRequestDto(
        int OrderId,
        int AmountPaid
    );
}
