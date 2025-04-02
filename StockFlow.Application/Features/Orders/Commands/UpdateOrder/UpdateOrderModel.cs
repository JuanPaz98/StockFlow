using StockFlow.Application.Features.Orders.Commands.CreateOrder;
using StockFlow.Application.Features.Orders.Dtos;

namespace StockFlow.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderModel
    {
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public IEnumerable<OrderDetailsIdDto> OrderDetails { get; set; }
    }
}
