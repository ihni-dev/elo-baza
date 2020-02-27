using EloBaza.Domain;
using EloBaza.Infrastructure.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EloBaza.Infrastructure.EntityFramework
{
    public class SubjectDbContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }

        public SubjectDbContext(DbContextOptions<SubjectDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SubjectEntityTypeConfiguration());
        }
    }
}