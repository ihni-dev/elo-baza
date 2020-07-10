using EloBaza.Domain.QuestionAggregate;
using EloBaza.Domain.SubjectAggregate;
using EloBaza.Infrastructure.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EloBaza.MigrationTool.DbContexts
{
    public class EloBazaDbContext : DbContext
    {
        public DbSet<Subject> Subjects { get; private set; } = null!;
        public DbSet<Question> Questions { get; private set; } = null!;

        public EloBazaDbContext(DbContextOptions<EloBazaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SubjectEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExamSessionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExplanationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentEntityTypeConfiguration());
        }
    }
}
