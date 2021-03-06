﻿using EloBaza.Domain.QuestionAggregate;
using EloBaza.Infrastructure.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EloBaza.Infrastructure.EntityFramework.DbContexts
{
    public class QuestionDbContext : DbContext
    {
        public DbSet<Question> Questions { get; private set; } = null!;

        public QuestionDbContext(DbContextOptions<QuestionDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new QuestionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionCategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionExamSessionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionTestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExplanationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentEntityTypeConfiguration());
        }
    }
}
