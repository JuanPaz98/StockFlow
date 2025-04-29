namespace StockFlow.Application.Features.Dtos.Payments
{
    public record PaymentResponseDto(
        int Id, 
        decimal AmountPaid, 
        DateTime PaymentDate
        );
}
