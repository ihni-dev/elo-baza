using EloBaza.MigrationTool.DbContexts;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.MigrationTool
{
    class MigrationService : IHostedService
    {
        private readonly ILogger<MigrationService> _logger;
        private readonly TelemetryClient _telemetryClient;
        private readonly IConfiguration _configuration;

        public MigrationService(ILogger<MigrationService> logger, TelemetryClient telemetryClient, IConfiguration configuration)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (_telemetryClient.StartOperation<RequestTelemetry>("Migrating database"))
            {
                _logger.LogInformation("Starting Database Migration Tool...");

                try
                {
                    await Migrate(cancellationToken);
                    _logger.LogInformation("Database migrated");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Could not migrate database");
                }
            }

            await StopAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Waiting for AI client flush.");
            _logger.LogInformation("Shutting down Migration Tool...");

            _telemetryClient.Flush();
            await Task.Delay(5000, cancellationToken);
        }

        private async Task Migrate(CancellationToken cancellationToken)
        {
            var dbContextFactory = new EloBazaDbContextDesignTimeFactory(_configuration);

            var migrated = false;
            var maxRetries = 10;
            var retries = 0;
            var retryDelay = TimeSpan.FromSeconds(5);

            do
            {
                try
                {
                    using var eloBazaDbContext = dbContextFactory.CreateDbContext(Array.Empty<string>());
                    eloBazaDbContext.Database.Migrate();
                    migrated = true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Migration failed");
                    _logger.LogError($"Waiting {retryDelay} for retry");
                    retries++;
                    await Task.Delay(5000, cancellationToken);
                }
            } while (!migrated && retries < maxRetries);

            if (!migrated)
                throw new ApplicationException("Could not migrate database");
        }
    }
}
