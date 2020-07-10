using EloBaza.Domain.QuestionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class AnswerEntityTypeConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable(nameof(Answer));

            builder.HasKey("Id");
            builder.Property("Id")
                .HasColumnName($"{nameof(Answer)}Id");

            builder.HasAlternateKey(a => a.Key);

            builder.HasOne<Question>(a => a.Question)
                .WithMany(q => q.Answers)
                .IsRequired(false)
                .HasForeignKey($"{nameof(Question)}Id")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
