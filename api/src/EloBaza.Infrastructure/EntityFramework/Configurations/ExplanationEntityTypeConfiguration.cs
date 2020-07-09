using EloBaza.Domain.Question;
using EloBaza.Domain.Subject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class ExplanationEntityTypeConfiguration : IEntityTypeConfiguration<Explanation>
    {
        public void Configure(EntityTypeBuilder<Explanation> builder)
        {
            builder.ToTable("Explanation");

            builder.HasKey("_id")
                .HasName("Id");

            builder.Property("_id")
                .HasColumnName($"ExplanationId");

            builder.Property(e => e.Key)
                .IsRequired(true);

            builder.HasAlternateKey(e => e.Key);

            builder.Property(e => e.Content)
                .HasMaxLength(ExamSession.ExamSessionNameMaxLength)
                .IsRequired(true);

            builder.HasOne(e => e.Question)
                .WithOne(s => s.Explanation)
                .HasForeignKey<Explanation>("QuestionId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
