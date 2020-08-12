using AutoMapper;
using EloBaza.MailService.Extensions;
using EloBaza.MailService.Mailing;
using EloBaza.MailService.Mailing.Config;
using EloBaza.MailService.Middleware;
using EloBaza.MailService.ServiceBusListener.NewUserRegistered;
using EloBaza.MailService.ServiceBusListener.NewUserRegistered.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Serilog;
using System.Reflection;

namespace EloBaza.MailService
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

            services.Configure<NewUserRegisteredServiceBusConfig>(Configuration.GetSection("ServiceBus:NotifyNewUserRegistered:MailService"))
                .AddHostedService<NewUserRegisteredServiceBusListener>()
                .Configure<SmtpConfig>(Configuration.GetSection("SmtpConfig"))
                .AddScoped<IMailService, Mailing.MailService>()
                .AddAutoMapper(typeof(Program).GetTypeInfo().Assembly)
                .AddSwagger()
                .AddCorsPolicies();

            services.AddAuthentication(AzureADB2CDefaults.BearerAuthenticationScheme)
                .AddAzureADB2CBearer(options => Configuration.Bind("AzureAdB2C", options));
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
                    endpoints.MapControllers();
                });
        }
    }
}
