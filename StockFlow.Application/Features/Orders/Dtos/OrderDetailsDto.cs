namespace StockFlow.Application.Features.Orders.Dtos
{
    public class OrderDetailsDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
