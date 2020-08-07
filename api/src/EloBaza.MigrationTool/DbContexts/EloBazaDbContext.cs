using EloBaza.Domain.QuestionAggregate;
using EloBaza.Domain.SubjectAggregate;
using EloBaza.Domain.UserAggregate;
using EloBaza.Infrastructure.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EloBaza.MigrationTool.DbContexts
{
    public class EloBazaDbContext : DbContext
    {
        public DbSet<Subject> Subjects { get; private set; } = null!;
        public DbSet<Question> Questions { get; private set; } = null!;
        public DbSet<User> Users { get; private set; } = null!;

        public EloBazaDbContext(DbContextOptions<EloBazaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SubjectEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExamSessionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionCategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionExamSessionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionTestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExplanationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}
