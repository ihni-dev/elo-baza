using EloBaza.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    class ExamSessionEntityTypeConfiguration : IEntityTypeConfiguration<ExamSession>
    {
        public void Configure(EntityTypeBuilder<ExamSession> builder)
        {
            builder.ToTable(nameof(ExamSession));

            builder.HasKey(es => es.Id);

            builder.Property(es => es.Id)
                .HasColumnName($"{nameof(ExamSession)}{nameof(ExamSession.Id)}");

            builder.Property(es => es.Name)
                .HasMaxLength(ExamSession.ExamSessionNameMaxLength)
                .IsRequired(true);

            builder.Property(es => es.Year)
                .IsRequired(true);

            builder.Property(es => es.Semester)
                .HasMaxLength(Enum.GetNames(typeof(Semester)).Max(n => n.Length))
                .IsRequired(true)
                .HasConversion(new EnumToStringConverter<Semester>());

            builder.Property(es => es.ResitNumber)
                .IsRequired(false);

            builder.HasOne(es => es.Subject)
                .WithMany(s => s.ExamSessions)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
