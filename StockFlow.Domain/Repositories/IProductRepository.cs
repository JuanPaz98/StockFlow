using System.Linq.Expressions;
using StockFlow.Api.Domain.Entities;

namespace StockFlow.Domain.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(ProductEntity product, CancellationToken cancellationToken = default);
        Task<ProductEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductEntity>> GetByCategoryId(int orderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductEntity>> FindAsync(Expression<Func<ProductEntity, bool>> predicate, CancellationToken cancellationToken = default);
        void Remove(ProductEntity product);
        void Update(ProductEntity product);
    }
}
