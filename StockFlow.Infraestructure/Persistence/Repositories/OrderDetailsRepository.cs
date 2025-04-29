using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StockFlow.Api.Domain.Entities;
using StockFlow.Domain.Repositories;
using StockFlow.Infrastructure.Persistence.configuration;

namespace StockFlow.Infrastructure.Persistence.Repositories
{
    public class OrderDetailsRepository(StockFlowContext context) : IOrderDetailsRepository
    {
        public async Task AddAsync(OrderDetailEntity orderDetails, CancellationToken cancellationToken = default) => await context.OrderDetails.AddAsync(orderDetails, cancellationToken);

        public async Task<IEnumerable<OrderDetailEntity>> FindAsync(Expression<Func<OrderDetailEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await context.OrderDetails.Where(predicate).ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<OrderDetailEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.OrderDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<OrderDetailEntity>> GetByOrderId(int orderId, CancellationToken cancellationToken = default)
        {
            return await context.OrderDetails
                 .AsNoTracking()
                 .Where(x => x.OrderId == orderId)
                 .ToListAsync(cancellationToken: cancellationToken);
        }

        public void Remove(OrderDetailEntity orderDetails) => context.OrderDetails.Remove(orderDetails);
        public void RemoveRange(IEnumerable<OrderDetailEntity> entities) => context.OrderDetails.RemoveRange(entities);
        public void Update(OrderDetailEntity orderDetails) => context.OrderDetails.Update(orderDetails);
    }
}
