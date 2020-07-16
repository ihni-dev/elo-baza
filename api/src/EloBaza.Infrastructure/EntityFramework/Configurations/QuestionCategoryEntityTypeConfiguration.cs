using EloBaza.Domain.QuestionAggregate;
using EloBaza.Domain.SubjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class QuestionCategoryEntityTypeConfiguration : IEntityTypeConfiguration<QuestionCategory>
    {
        public void Configure(EntityTypeBuilder<QuestionCategory> builder)
        {
            builder.ToTable($"{nameof(Question)}{nameof(Category)}");

            builder.HasKey(qc => new { qc.QuestionId, qc.CategoryId });

            builder.HasOne<Question>()
                .WithMany()
                .HasForeignKey($"{nameof(Question)}Id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey($"{nameof(Category)}Id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
