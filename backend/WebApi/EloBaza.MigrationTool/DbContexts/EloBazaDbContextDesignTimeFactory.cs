using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace EloBaza.MigrationTool.DbContexts
{
    class EloBazaDbContextDesignTimeFactory : IDesignTimeDbContextFactory<EloBazaDbContext>
    {
        private readonly IConfiguration _configuration;

        public EloBazaDbContextDesignTimeFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EloBazaDbContext CreateDbContext(string[] args)
        {
            var connectionString = _configuration.GetConnectionString("DB");
            Log.Information($"Using connection string: {connectionString}");

            var builder = new DbContextOptionsBuilder<EloBazaDbContext>()
                .UseSqlServer(connectionString, sqlServerOptionsAction: o =>
                {
                    o.MigrationsAssembly(typeof(EloBazaDbContextDesignTimeFactory).Assembly.FullName);
                })
                .EnableDetailedErrors()
                .LogTo(Log.Verbose, Microsoft.Extensions.Logging.LogLevel.Trace)
                .LogTo(Log.Debug, Microsoft.Extensions.Logging.LogLevel.Debug)
                .LogTo(Log.Information, Microsoft.Extensions.Logging.LogLevel.Information)
                .LogTo(Log.Warning, Microsoft.Extensions.Logging.LogLevel.Warning)
                .LogTo(Log.Error, Microsoft.Extensions.Logging.LogLevel.Error)
                .LogTo(Log.Fatal, Microsoft.Extensions.Logging.LogLevel.Critical);

            return new EloBazaDbContext(builder.Options);
        }
    }
}
