using EloBaza.MigrationTool.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EloBaza.MigrationTool
{
    public sealed class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting Database Migration Tool...");

            var dbContextFactory = new EloBazaDbContextDesignTimeFactory();

            var migrated = false;
            var maxRetries = 10;
            var retries = 0;
            var retryDelay = TimeSpan.FromSeconds(5);

            do
            {
                try
                {
                    using var eloBazaDbContext = dbContextFactory.CreateDbContext(args);
                    eloBazaDbContext.Database.Migrate();
                    migrated = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Migration failed: {ex.Message}");
                    Console.WriteLine($"Waiting {retryDelay} for retry");
                    retries++;
                    await Task.Delay(retryDelay);
                }
            } while (!migrated && retries < maxRetries);

            if (migrated)
                Console.WriteLine("Done");
            else
                Console.WriteLine("Could not migrate database");
        }
    }
}
