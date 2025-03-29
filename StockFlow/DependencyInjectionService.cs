using Microsoft.OpenApi.Models;

namespace StockFlow.Api
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            return services;
        }
    }
}
