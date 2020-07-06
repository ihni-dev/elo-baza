using EloBaza.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable(nameof(Question));

            builder.HasKey(q => q.Id);

            builder.Property(q => q.Id)
                .HasColumnName($"{nameof(Question)}{nameof(Question.Id)}");

            builder.Property(q => q.Content)
                .IsRequired(true);

            builder.Property(q => q.IsPublished)
                .IsRequired(true);

            builder.HasOne(q => q.Subject)
                .WithMany(s => s.Questions)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(q => q.Category)
                .WithMany(c => c.Questions)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(q => q.ExamSession)
                .WithMany(es => es.Questions)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
