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
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DB");
            Console.WriteLine($"Using connection string: {connectionString}");

            var builder = new DbContextOptionsBuilder<SubjectDbContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(SubjectDbContextDesignTimeFactory).Assembly.FullName));

            return new SubjectDbContext(builder.Options);
        }
    }
}
