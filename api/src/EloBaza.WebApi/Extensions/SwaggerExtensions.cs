using EloBaza.Application.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace EloBaza.WebApi.Extensions
{
    static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var webApiXmlFile = $"{typeof(SwaggerExtensions).GetTypeInfo().Assembly.GetName().Name}.xml";
            var webApiXmlPath = Path.Combine(AppContext.BaseDirectory, webApiXmlFile);

            var applicationXmlFile = $"{typeof(ApplicationServiceCollectionExtension).GetTypeInfo().Assembly.GetName().Name}.xml";
            var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXmlFile);

            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "EloBaza API", Version = "v1" });

                options.IncludeXmlComments(webApiXmlPath);
                options.IncludeXmlComments(applicationXmlPath);
            })
            .AddSwaggerGenNewtonsoftSupport();
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            return app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "EloBaza API v1");
                    options.DisplayRequestDuration();
                });
        }
    }
}
