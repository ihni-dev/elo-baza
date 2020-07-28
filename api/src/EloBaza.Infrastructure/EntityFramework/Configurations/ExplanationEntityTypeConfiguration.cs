using EloBaza.Domain.QuestionAggregate;
using EloBaza.Domain.SubjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class ExplanationEntityTypeConfiguration : IEntityTypeConfiguration<Explanation>
    {
        public void Configure(EntityTypeBuilder<Explanation> builder)
        {
            builder.ToTable(nameof(Explanation));

            builder.HasKey("Id");
            builder.Property("Id")
                .HasColumnName($"{nameof(Explanation)}Id");

            builder.Property(e => e.Key)
                .HasColumnName($"{nameof(Explanation)}Key");
            builder.HasAlternateKey(e => e.Key);

            builder.Property(e => e.Content)
                .HasMaxLength(ExamSession.ExamSessionNameMaxLength)
                .IsRequired(true);

            builder.HasOne<Question>(e => e.Question)
                .WithMany(q => q.Explanations)
                .IsRequired(false)
                .HasForeignKey($"{nameof(Question)}Id")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
