using StockFlow.Application.Features.Orders.Commands.CreateOrder;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.Features.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdModel
    {
        public int CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; } = PaymentStatus.Pending.ToString();
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public IEnumerable<OrderDetailsModel> OrderDetails { get; set; }

    }
}
