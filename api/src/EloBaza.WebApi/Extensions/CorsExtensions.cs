using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EloBaza.WebApi.Extensions
{
    public static class CorsExtensions
    {
        private const string DevelopmentPolicy = "Development";
        private const string ProductionPolicy = "Production";

        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy(DevelopmentPolicy,
                    builder =>
                    {
                        builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });

                //options.AddPolicy(ProductionPolicy)
            });
        }

        public static IApplicationBuilder UseDevelopmentCors(this IApplicationBuilder builder)
        {
            return builder.UseCors(DevelopmentPolicy);
        }

        public static IApplicationBuilder UseProductionCors(this IApplicationBuilder builder)
        {
            return builder.UseCors(ProductionPolicy);
        }
    }
}
