using StockFlow.Application.Features.Orders.Commands.CreateOrder;
using StockFlow.Application.Features.Orders.Dtos;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.Features.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public IEnumerable<OrderDetailsIdDto> OrderDetails { get; set; }

    }
}
