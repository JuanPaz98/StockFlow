using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Repositories;
using StockFlow.Infrastructure.Persistence.configuration;

namespace StockFlow.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository(StockFlowContext context) : ICategoryRepository
    {
        public async Task AddAsync(CategoryEntity category, CancellationToken cancellationToken = default) => await context.Categories.AddAsync(category, cancellationToken);

        public async Task<IEnumerable<CategoryEntity>> FindAsync(Expression<Func<CategoryEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await context.Categories
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<CategoryEntity>> GetAllAsync(CancellationToken cancelationToken = default)
        {
            return await context.Categories.ToListAsync(cancelationToken);
        }

        public async Task<CategoryEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.Categories
                 .AsNoTracking()
                 .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public void Remove(CategoryEntity category) => context.Remove(category);

        public void Update(CategoryEntity category) => context.Update(category);
    }
}
