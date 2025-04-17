using StockFlow.Api.Domain.Entities;

namespace StockFlow.Domain.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductEntity> Products { get; set; }
    }
}
