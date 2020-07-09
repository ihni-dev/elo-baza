using EloBaza.Domain.Question;
using EloBaza.Domain.Subject;
using EloBaza.Infrastructure.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EloBaza.MigrationTool.DbContexts
{
    public class EloBazaDbContext : DbContext
    {
        public DbSet<SubjectAggregate> Subjects { get; private set; } = null!;
        public DbSet<QuestionAggregate> Questions { get; private set; } = null!;

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
