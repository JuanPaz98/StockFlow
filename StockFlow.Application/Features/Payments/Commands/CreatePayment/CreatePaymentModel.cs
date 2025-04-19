namespace StockFlow.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentModel
    {
        public int OrderId { get; set; }
        public int AmountPaid { get; set; }
    }
}
