using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StockFlow.Api.Domain.Entities;
using StockFlow.Domain.Repositories;
using StockFlow.Infrastructure.Persistence.configuration;

namespace StockFlow.Infrastructure.Persistence.Repositories
{
    public class OrderRepository(StockFlowContext context) : IOrderRepository
    {
        public async Task AddAsync(OrderEntity order, CancellationToken cancellationToken = default) => await context.Orders.AddAsync(order);

        public async Task<IEnumerable<OrderEntity>> FindAsync(Expression<Func<OrderEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await context.Orders.Where(predicate).ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<OrderEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Orders
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<OrderEntity>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await context.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == customerId)
                .ToListAsync(cancellationToken);
        }

        public async Task<OrderEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<OrderEntity?> GetByIdWithOrderDetailsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.Orders
                .Include(o => o.OrderDetails)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public void Remove(OrderEntity order) => context.Orders.Remove(order);

        public void Update(OrderEntity order) => context.Orders.Update(order);
    }
}
