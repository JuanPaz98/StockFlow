using StockFlow.Application.Features.Orders.Dtos;

namespace StockFlow.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdModel
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
