using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace EloBaza.MailService.Extensions
{
    static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var mailServiceXmlFile = $"{typeof(SwaggerExtensions).GetTypeInfo().Assembly.GetName().Name}.xml";
            var mailServiceXmlPath = Path.Combine(AppContext.BaseDirectory, mailServiceXmlFile);

            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "EloBaza API", Version = "v1" });

                options.IncludeXmlComments(mailServiceXmlFile);
            });
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
