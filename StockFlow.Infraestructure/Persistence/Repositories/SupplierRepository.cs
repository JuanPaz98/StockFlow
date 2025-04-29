using Microsoft.EntityFrameworkCore;
using StockFlow.Api.Domain.Entities;
using StockFlow.Domain.Repositories;
using StockFlow.Infrastructure.Persistence.configuration;

namespace StockFlow.Infrastructure.Persistence.Repositories
{
    public class SupplierRepository(StockFlowContext context) : ISupplierRepository
    {
        private readonly StockFlowContext _context = context;

        public async Task AddAsync(SupplierEntity supply, CancellationToken cancellationToken = default) => await _context.Suppliers.AddAsync(supply, cancellationToken);

        public async Task<IEnumerable<SupplierEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Suppliers
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public Task<SupplierEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Suppliers
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public void Remove(SupplierEntity supply) => _context.Suppliers.Remove(supply);

        public void Update(SupplierEntity supply) => _context.Suppliers.Update(supply);
    }
}
