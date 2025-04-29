using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using StockFlow.Application.Cache;
using StockFlow.Application.Interfaces;
using StockFlow.Domain.Repositories;
using StockFlow.Infraestructure.Persistence.Redis;
using StockFlow.Infrastructure.Persistence.configuration;
using StockFlow.Infrastructure.Persistence.Repositories;

namespace StockFlow.Infraestructure
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            // SQL Server
            //services.AddDbContext<StockFlowContext>(options =>
            //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Postgres
            services.AddDbContext<StockFlowContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            //Redis
            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection") ?? ""));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ICacheService, RedisCacheService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
