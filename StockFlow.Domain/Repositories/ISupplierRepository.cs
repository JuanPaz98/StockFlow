using StockFlow.Api.Domain.Entities;

namespace StockFlow.Domain.Repositories
{
    public interface ISupplierRepository
    {
        Task AddAsync(SupplierEntity supply, CancellationToken cancellationToken = default);
        Task<SupplierEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<SupplierEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        void Remove(SupplierEntity supply);
        void Update(SupplierEntity supply);
    }
}
