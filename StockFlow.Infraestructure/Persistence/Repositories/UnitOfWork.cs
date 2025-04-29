using StockFlow.Application.Interfaces;
using StockFlow.Domain.Repositories;
using StockFlow.Infrastructure.Persistence.configuration;

namespace StockFlow.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork(StockFlowContext context) : IUnitOfWork
    {
        private ICategoryRepository? _categoryRepository;
        private ICustomerRepository? _customerRepository;
        private IOrderRepository? _orderRepository;
        private IOrderDetailsRepository? _orderDetailsRepository;
        private IPaymentRepository? _paymentRepository;
        private IProductRepository? _productRepository;
        private ISupplierRepository? _supplierRepository;

        public ICustomerRepository Customers => _customerRepository ??= new CustomerRepository(context);

        public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(context);

        public IOrderRepository Orders => _orderRepository ??= new OrderRepository(context);
        public IOrderDetailsRepository OrderDetails => _orderDetailsRepository ??= new OrderDetailsRepository(context);

        public IPaymentRepository Payments => _paymentRepository ??= new PaymentRepository(context);

        public IProductRepository Products => _productRepository ??= new ProductRepository(context);

        public ISupplierRepository Suppliers => _supplierRepository ??= new SupplierRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await context.SaveChangesAsync(cancellationToken);      
    }
}
