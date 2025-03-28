using Microsoft.Extensions.DependencyInjection;

namespace StockFlow.Domain
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services;
        }
    }
}
