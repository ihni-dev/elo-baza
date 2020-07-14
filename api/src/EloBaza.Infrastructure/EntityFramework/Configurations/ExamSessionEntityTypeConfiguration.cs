using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SubjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class ExamSessionEntityTypeConfiguration : IEntityTypeConfiguration<ExamSession>
    {
        public void Configure(EntityTypeBuilder<ExamSession> builder)
        {
            builder.ToTable(nameof(ExamSession));

            builder.HasKey("Id");
            builder.Property("Id")
                .HasColumnName($"{nameof(ExamSession)}Id");

            builder.Property(es => es.Key)
                .HasColumnName($"{nameof(ExamSession)}Key");
            builder.HasAlternateKey(es => es.Key);

            builder.Property(es => es.Name)
                .HasMaxLength(ExamSession.ExamSessionNameMaxLength)
                .IsRequired(true);

            builder.Property(es => es.Semester)
                .HasMaxLength(Semester.NameMaxLength)
                .IsRequired(true)
                .HasConversion(v => v.ToString(), v => Enumeration.FromDisplayName<Semester>(v));

            builder.HasOne<Subject>(es => es.Subject)
                .WithMany(s => s.ExamSessions)
                .IsRequired(false)
                .HasForeignKey($"{nameof(Subject)}Id")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
