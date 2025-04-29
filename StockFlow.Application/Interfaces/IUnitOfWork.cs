using StockFlow.Domain.Repositories;

namespace StockFlow.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IPaymentRepository Payments { get; }
        IProductRepository Products { get; }
        ISupplierRepository Suppliers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
