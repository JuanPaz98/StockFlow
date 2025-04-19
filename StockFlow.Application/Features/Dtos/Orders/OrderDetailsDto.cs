namespace StockFlow.Application.Features.Dtos.Orders
{
    public class OrderDetailsDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
