using EloBaza.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace EloBaza.MigrationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Database Migration Tool...");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            string connectionString = configuration.GetConnectionString("DB");
            Console.WriteLine($"Using connection string: {connectionString}");

            var optionsBuilder = DbContextOptionsBuilderProvider<SubjectDbContext>.GetDbContextOptionsBuilder(connectionString);

            using (SubjectDbContext sc = new SubjectDbContext(optionsBuilder.Options))
            {
                sc.Database.Migrate();
            }

            Console.WriteLine("Done");
        }
    }
}
