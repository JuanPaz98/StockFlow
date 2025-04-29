using System.Linq.Expressions;
using StockFlow.Domain.Entities;

namespace StockFlow.Domain.Repositories
{
    public interface IPaymentRepository
    {
        Task AddAsync(PaymentEntity payment, CancellationToken cancellationToken = default);
        Task<PaymentEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<PaymentEntity>> GetByOrderIdAsync(int orderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<PaymentEntity>> FindAsync(Expression<Func<PaymentEntity, bool>> predicate, CancellationToken cancellationToken = default);
        void Update(PaymentEntity payment);
    }
}
