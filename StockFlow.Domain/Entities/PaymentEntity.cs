using StockFlow.Api.Domain.Entities;

namespace StockFlow.Domain.Entities
{
    public class PaymentEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }

        public OrderEntity Order { get; set; }
    }
}
