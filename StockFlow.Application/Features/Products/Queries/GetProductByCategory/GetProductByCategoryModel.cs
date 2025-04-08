namespace StockFlow.Application.Features.Products.Queries.GetProductByCategory
{
    public class GetProductByCategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string? Category { get; set; }
    }
}
