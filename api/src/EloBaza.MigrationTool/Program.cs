using EloBaza.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EloBaza.MigrationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = configurationBuilder.Build();
            string connectionString = configuration.GetConnectionString("DB");

            var optionsBuilder = new DbContextOptionsBuilder<SubjectDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (SubjectDbContext sc = new SubjectDbContext(optionsBuilder.Options))
            {
                sc.Database.Migrate();
            }
        }
    }
}
