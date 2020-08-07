using EloBaza.Domain.UserAggregate;
using EloBaza.Infrastructure.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EloBaza.Infrastructure.EntityFramework.DbContexts
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; private set; } = null!;

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}