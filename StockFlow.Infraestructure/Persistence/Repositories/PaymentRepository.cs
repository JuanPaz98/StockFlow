using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Repositories;
using StockFlow.Infrastructure.Persistence.configuration;

namespace StockFlow.Infrastructure.Persistence.Repositories
{
    public class PaymentRepository(StockFlowContext context) : IPaymentRepository
    {
        public async Task AddAsync(PaymentEntity payment, CancellationToken cancellationToken = default) => await context.Payments.AddAsync(payment);

        public async Task<IEnumerable<PaymentEntity>> FindAsync(Expression<Func<PaymentEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await context.Payments.Where(predicate).ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<PaymentEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.Payments
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<PaymentEntity>> GetByOrderIdAsync(int orderId, CancellationToken cancellationToken = default)
        {
            return await context.Payments
                .AsNoTracking()
                .Where(p => p.OrderId == orderId)
                .ToListAsync(cancellationToken);
        }

        public void Update(PaymentEntity payment) => context.Payments.Update(payment);
    }
}
