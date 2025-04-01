namespace StockFlow.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Category { get; set; }
    }
}
