using EloBaza.Domain.Question;
using EloBaza.Domain.Subject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<QuestionAggregate>
    {
        public void Configure(EntityTypeBuilder<QuestionAggregate> builder)
        {
            builder.ToTable("Question");

            builder.HasKey("_id")
                .HasName("Id");

            builder.Property("_id")
                .HasColumnName($"QuestionId");

            builder.Property(q => q.Key)
                .IsRequired(true);

            builder.HasAlternateKey(q => q.Key);

            builder.Property(q => q.Content)
                .IsRequired(true);

            builder.Property(q => q.IsPublished)
                .IsRequired(true);

            builder.Property("_subjectId")
                .IsRequired(false);
            builder.HasOne<SubjectAggregate>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("_subjectId")
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property("_categoryId")
                .IsRequired(false);
            builder.HasOne<Category>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("_categoryId")
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property("_examSessionId")
                .IsRequired(false);
            builder.HasOne<ExamSession>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("_examSessionId")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
