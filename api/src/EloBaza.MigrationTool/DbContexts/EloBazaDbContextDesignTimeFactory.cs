using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace EloBaza.MigrationTool.DbContexts
{
    class EloBazaDbContextDesignTimeFactory : IDesignTimeDbContextFactory<EloBazaDbContext>
    {
        public EloBazaDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DB");
            Console.WriteLine($"Using connection string: {connectionString}");

            var builder = new DbContextOptionsBuilder<EloBazaDbContext>()
                .UseSqlServer(connectionString, sqlServerOptionsAction: o =>
                {
                    o.MigrationsAssembly(typeof(EloBazaDbContextDesignTimeFactory).Assembly.FullName);
                    o.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });

            return new EloBazaDbContext(builder.Options);
        }
    }
}
