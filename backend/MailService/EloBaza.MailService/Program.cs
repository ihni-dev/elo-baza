using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace EloBaza.MailService
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetAppConfiguration();
            InitLogger(configuration);

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args, configuration).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddConfiguration(configuration);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();

        private static IConfiguration GetAppConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets(typeof(Program).Assembly)
                .Build();

            return new ConfigurationBuilder()
                .AddAzureAppConfiguration(options =>
                {
                    options.Connect(config["ConnectionStrings:AppConfig"])
                        .Select(KeyFilter.Any, Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development");
                })
                .Build();
        }

        private static void InitLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
