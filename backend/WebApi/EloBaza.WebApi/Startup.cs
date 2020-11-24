using AutoMapper;
using EloBaza.Application.IoC;
using EloBaza.Infrastructure.EntityFramework.IoC;
using EloBaza.WebApi.ApplicationInsights;
using EloBaza.WebApi.Extensions;
using EloBaza.WebApi.Middleware;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
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
                .AddControllers();

            services.AddHealthChecks()
                    .AddSqlServer(Configuration.GetConnectionString("DB"));

            services.AddInfrastructureServices(Configuration)
                .AddApplicationServices()
                //.AddServiceBusListenerServices(Configuration)
                .AddAutoMapper(typeof(Program).GetTypeInfo().Assembly)
                .AddSwagger()
                .AddCorsPolicies();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration, "AzureAdB2C");

            services.AddApplicationInsightsTelemetry()
                .AddSingleton<ITelemetryInitializer, CloudRoleNameInitializer>();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                app.UseHsts()
                    .UseProductionCors();
            }
            else
            {
                app.UseDeveloperExceptionPage()
                    .UseDevelopmentCors()
                    .UseSwaggerDocumentation();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>()
                .UseHttpsRedirection()
                .UseSerilogRequestLogging()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapHealthChecks("/health");
                    endpoints.MapControllers();
                });
        }
    }
}
