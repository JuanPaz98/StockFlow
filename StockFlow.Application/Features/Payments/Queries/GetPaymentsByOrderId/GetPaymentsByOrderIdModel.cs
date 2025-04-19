namespace StockFlow.Application.Features.Payments.Queries.GetPaymentsByOrderId
{
    public class GetPaymentsByOrderIdModel
    {
        public int Id { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
