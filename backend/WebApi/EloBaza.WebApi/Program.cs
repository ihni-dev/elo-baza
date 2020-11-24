using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace EloBaza.WebApi
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            var configuration = GetAppConfiguration();
            CreateHostBuilder(args, configuration).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddConfiguration(configuration);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((context, services, config) =>
                {
                    var telemetryConfiguration = services.GetRequiredService<TelemetryConfiguration>();
                    config.ReadFrom.Configuration(configuration);

                    if (!string.IsNullOrWhiteSpace(configuration.GetValue<string>("ApplicationInsights:InstrumentationKey")))
                        config.WriteTo.ApplicationInsights(
                            services.GetRequiredService<TelemetryConfiguration>(),
                            TelemetryConverter.Traces);
                });

        private static IConfiguration GetAppConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? throw new InvalidOperationException("ASPNETCORE_ENVIRONMENT env variable is not defined");

            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets(typeof(Program).Assembly)
                .Build();

            return new ConfigurationBuilder()
                .AddAzureAppConfiguration(options =>
                    {
                        options.Connect(config["ConnectionStrings:AppConfig"])
                            .Select(KeyFilter.Any, environment);
                    })
                .Build();
        }
    }
}
