using Microsoft.EntityFrameworkCore;

namespace EloBaza.MigrationTool
{
    static class DbContextOptionsBuilderProvider<T> where T : DbContext
    {
        public static DbContextOptionsBuilder<T> GetDbContextOptionsBuilder(string connectionString)
        {
            return new DbContextOptionsBuilder<T>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(DbContextOptionsBuilderProvider<T>).Assembly.FullName));
        }
    }
}
