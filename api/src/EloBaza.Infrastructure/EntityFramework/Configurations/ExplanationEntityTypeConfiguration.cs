using EloBaza.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    class ExplanationEntityTypeConfiguration : IEntityTypeConfiguration<Explanation>
    {
        public void Configure(EntityTypeBuilder<Explanation> builder)
        {
            builder.ToTable(nameof(Explanation));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName($"{nameof(Explanation)}{nameof(Explanation.Id)}");

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
