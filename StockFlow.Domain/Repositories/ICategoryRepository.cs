using System.Linq.Expressions;
using StockFlow.Domain.Entities;

namespace StockFlow.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<CategoryEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoryEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoryEntity>> FindAsync(Expression<Func<CategoryEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task AddAsync(CategoryEntity category, CancellationToken cancellationToken = default);
        void Update(CategoryEntity category);
        void Remove(CategoryEntity category);
    }
}
