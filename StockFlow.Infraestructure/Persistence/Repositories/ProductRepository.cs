using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StockFlow.Api.Domain.Entities;
using StockFlow.Domain.Repositories;
using StockFlow.Infrastructure.Persistence.configuration;

namespace StockFlow.Infrastructure.Persistence.Repositories
{
    public class ProductRepository(StockFlowContext context) : IProductRepository
    {
        public async Task AddAsync(ProductEntity product, CancellationToken cancellationToken = default) => await context.Products.AddAsync(product, cancellationToken);

        public async Task<IEnumerable<ProductEntity>> FindAsync(Expression<Func<ProductEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await context.Products.Where(predicate).ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<ProductEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<ProductEntity>> GetByCategoryId(int categoryId, CancellationToken cancellationToken = default)
        {
            return await context.Products
                .AsNoTracking()
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync(cancellationToken);
        }

        public void Remove(ProductEntity product) => context.Products.Remove(product);

        public void Update(ProductEntity product) => context.Products.Update(product);
    }
}
