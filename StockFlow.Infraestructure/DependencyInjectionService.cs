using Microsoft.Extensions.DependencyInjection;

namespace StockFlow.Infraestructure
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            return services;
        }
    }
}
