using System.Linq.Expressions;
using StockFlow.Api.Domain.Entities;

namespace StockFlow.Domain.Repositories
{
    public interface IOrderDetailsRepository
    {
        Task AddAsync(OrderDetailEntity orderDetails, CancellationToken cancellationToken = default);
        Task<OrderDetailEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderDetailEntity>> FindAsync(Expression<Func<OrderDetailEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderDetailEntity>> GetByOrderId(int orderId, CancellationToken cancellationToken = default);
        void Remove(OrderDetailEntity orderDetails);
        void RemoveRange(IEnumerable<OrderDetailEntity> orderDetails);
        void Update(OrderDetailEntity orderDetails);
    }
}
