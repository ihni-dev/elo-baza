using EloBaza.Domain.QuestionAggregate;
using EloBaza.Domain.SubjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class QuestionExamSessionEntityTypeConfiguration : IEntityTypeConfiguration<QuestionExamSession>
    {
        public void Configure(EntityTypeBuilder<QuestionExamSession> builder)
        {
            builder.ToTable($"{nameof(Question)}{nameof(ExamSession)}");

            builder.HasKey(qc => new { qc.QuestionId, qc.ExamSessionId });

            builder.HasOne<Question>()
                .WithMany()
                .HasForeignKey($"{nameof(Question)}Id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<ExamSession>()
                .WithMany()
                .HasForeignKey($"{nameof(ExamSession)}Id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
