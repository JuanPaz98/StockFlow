using Microsoft.EntityFrameworkCore;
using StockFlow.Api.Domain.Entities;
using StockFlow.Domain.Repositories;
using StockFlow.Infrastructure.Persistence.configuration;

namespace StockFlow.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StockFlowContext _context;

        public CustomerRepository(StockFlowContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CustomerEntity customer, CancellationToken cancellationToken = default) => await _context.Customers.AddAsync(customer, cancellationToken);

        public async Task<IEnumerable<CustomerEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<CustomerEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _context.Customers.FindAsync(id, cancellationToken);

        public void Remove(CustomerEntity customer) => _context.Customers.Remove(customer);

        public void Update(CustomerEntity customer) => _context.Customers.Update(customer);
    }
}
