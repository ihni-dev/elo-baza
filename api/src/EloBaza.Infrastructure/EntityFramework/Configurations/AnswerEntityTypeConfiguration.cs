using EloBaza.Domain.Question;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class AnswerEntityTypeConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answer");

            builder.HasKey("_id")
                .HasName("Id");

            builder.Property("_id")
                .HasColumnName($"AnswerId");

            builder.Property(a => a.Key)
                .IsRequired(true);

            builder.HasAlternateKey(a => a.Key);

            builder.Property(a => a.Content)
                .IsRequired(true);

            builder.Property(a => a.IsValid)
                .IsRequired(true);

            builder.HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
