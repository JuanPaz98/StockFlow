using StockFlow.Domain.Enums;

namespace StockFlow.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderModel
    {
        public int CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; } = PaymentStatus.Pending.ToString();
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public IEnumerable<OrderDetailsModel> OrderDetails { get; set; }
        
    }

    public class OrderDetailsModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
