using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.Subject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class ExamSessionEntityTypeConfiguration : IEntityTypeConfiguration<ExamSession>
    {
        public void Configure(EntityTypeBuilder<ExamSession> builder)
        {
            builder.ToTable("ExamSession");

            builder.HasKey("_id")
                .HasName("Id");

            builder.Property("_id")
                .HasColumnName($"ExamSessionId");

            builder.Property(es => es.Key)
                .IsRequired(true);

            builder.HasAlternateKey(es => es.Key);

            builder.Property(es => es.Name)
                .HasMaxLength(ExamSession.ExamSessionNameMaxLength)
                .IsRequired(true);

            builder.Property(es => es.Year)
                .IsRequired(true);

            builder.Property(es => es.Semester)
                .HasMaxLength(Semester.NameMaxLength)
                .IsRequired(true)
                .HasConversion(v => v.ToString(), v => Enumeration.FromDisplayName<Semester>(v));

            builder.Property(es => es.ResitNumber)
                .IsRequired(false);

            builder.HasOne(es => es.Subject)
                .WithMany(s => s.ExamSessions)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
