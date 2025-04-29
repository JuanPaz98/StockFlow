using System.Linq.Expressions;
using StockFlow.Api.Domain.Entities;

namespace StockFlow.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(OrderEntity order, CancellationToken cancellationToken = default);
        Task<OrderEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderEntity>> FindAsync(Expression<Func<OrderEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderEntity>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
        Task<OrderEntity> GetByIdWithOrderDetailsAsync(int id, CancellationToken cancellationToken = default);
        void Remove(OrderEntity order);
        void Update(OrderEntity order);
    }
}
