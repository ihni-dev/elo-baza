using AutoMapper;
using EloBaza.Application.IoC;
using EloBaza.Infrastructure.EntityFramework.IoC;
using EloBaza.WebApi.Extensions;
using EloBaza.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Serilog;
using System.Reflection;

namespace EloBaza.WebApi
{
    class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor()
                .AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddInfrastructureServices(Configuration)
                .AddApplicationServices()
                .AddAutoMapper(typeof(Program).GetTypeInfo().Assembly)
                .AddSwagger()
                .AddCorsPolicies();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage()
                    .UseDevelopmentCors()
                    .UseSwaggerDocumentation();
            }
            else
            {
                app.UseHsts();
                app.UseProductionCors();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>()
                .UseHttpsRedirection()
                .UseSerilogRequestLogging()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
