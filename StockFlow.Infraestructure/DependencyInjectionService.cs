using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockFlow.Api.Infrastructure.Persistence;
using StockFlow.Application.Interfaces;
using StockFlow.Infraestructure.Persistence.Repositories;

namespace StockFlow.Infraestructure
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StockFlowContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
