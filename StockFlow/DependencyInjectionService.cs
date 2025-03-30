using Microsoft.OpenApi.Models;

namespace StockFlow.Api
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "StockFlow API",
                    Version = "v1",
                    Description = "API for managing inventory, customers, and sales.",
                });
            });

            return services;
        }
    }
}
