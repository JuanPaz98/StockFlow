using StockFlow.Api.Domain.Entities;

namespace StockFlow.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task AddAsync(CustomerEntity customer, CancellationToken cancellationToken = default);
        Task<CustomerEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CustomerEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        void Remove(CustomerEntity customer);
        void Update(CustomerEntity customer);
    }
}
