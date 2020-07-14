using EloBaza.Domain.QuestionAggregate;
using EloBaza.Domain.SubjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable(nameof(Question));

            builder.HasKey("Id");
            builder.Property("Id")
                .HasColumnName($"{nameof(Question)}Id");

            builder.Property(q => q.Key)
                .HasColumnName($"{nameof(Question)}Key");
            builder.HasAlternateKey(q => q.Key);

            builder.Property($"{nameof(Subject)}Id")
                .HasColumnName($"{nameof(Subject)}Id")
                .IsRequired(false);
            builder.HasOne<Subject>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey($"{nameof(Subject)}Id")
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property($"{nameof(Category)}Id")
                .HasColumnName($"{nameof(Category)}Id")
                .IsRequired(false);
            builder.HasOne<Category>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey($"{nameof(Category)}Id")
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property($"{nameof(ExamSession)}Id")
                .HasColumnName($"{nameof(ExamSession)}Id")
                .IsRequired(false);
            builder.HasOne<ExamSession>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey($"{nameof(ExamSession)}Id")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
