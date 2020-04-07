using EloBaza.Infrastructure.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace EloBaza.MigrationTool.DesignTimeDbContextFactiories
{
    class SubjectDbContextDesignTimeFactory : IDesignTimeDbContextFactory<SubjectDbContext>
    {
        public SubjectDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DB");
            Console.WriteLine($"Using connection string: {connectionString}");

            var builder = new DbContextOptionsBuilder<SubjectDbContext>()
                .UseSqlServer(connectionString, sqlServerOptionsAction: o =>
                {
                    o.MigrationsAssembly(typeof(SubjectDbContextDesignTimeFactory).Assembly.FullName);
                    o.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });

            return new SubjectDbContext(builder.Options);
        }
    }
}
