﻿using EloBaza.Domain.SubjectAggregate;
using EloBaza.Infrastructure.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EloBaza.Infrastructure.EntityFramework.DbContexts
{
    public class SubjectDbContext : DbContext
    {
        public DbSet<Subject> Subjects { get; private set; } = null!;

        public SubjectDbContext(DbContextOptions<SubjectDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SubjectEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExamSessionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TestEntityTypeConfiguration());
        }
    }
}