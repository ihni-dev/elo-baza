using EloBaza.MigrationTool.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace EloBaza.MigrationTool
{
    public sealed class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = GetAppConfiguration();
            using var host = CreateHostBuilder(args, configuration)
                .Build();

            await host.StartAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddConfiguration(configuration);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<MigrationService>()
                        .AddApplicationInsightsTelemetryWorkerService()
                        .AddSingleton<ITelemetryInitializer, CloudRoleNameInitializer>();
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
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? throw new InvalidOperationException("DOTNET_ENVIRONMENT env variable is not defined");

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
