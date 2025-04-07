using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using StockFlow.Api.Infrastructure.Persistence;
using StockFlow.Application.Interfaces;
using StockFlow.Infraestructure.Persistence.Redis;
using StockFlow.Infraestructure.Persistence.Repositories;

namespace StockFlow.Infraestructure
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            // SQL Server
            services.AddDbContext<StockFlowContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Redis
            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection") ?? ""));

            services.AddScoped<ICacheService, RedisCacheService>();

            return services;
        }
    }
}
