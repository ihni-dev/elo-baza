using EloBaza.Domain.QuestionAggregate;
using EloBaza.Domain.QuestionAggregate.Links;
using EloBaza.Domain.SubjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class QuestionTestEntityTypeConfiguration : IEntityTypeConfiguration<QuestionTest>
    {
        public void Configure(EntityTypeBuilder<QuestionTest> builder)
        {
            builder.ToTable($"{nameof(Question)}{nameof(Test)}");

            builder.HasKey(qc => new { qc.QuestionId, qc.TestId });

            builder.HasOne<Question>()
                .WithMany()
                .HasForeignKey($"{nameof(Question)}Id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Test>()
                .WithMany()
                .HasForeignKey($"{nameof(Test)}Id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
