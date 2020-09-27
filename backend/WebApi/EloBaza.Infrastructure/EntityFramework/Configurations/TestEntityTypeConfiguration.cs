using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SubjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class TestEntityTypeConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable(nameof(Test));

            builder.HasKey("Id");
            builder.Property("Id")
                .HasColumnName($"{nameof(Test)}Id");

            builder.Property(es => es.Key)
                .HasColumnName($"{nameof(Test)}Key");
            builder.HasAlternateKey(es => es.Key);

            builder.Property(es => es.Name)
                .HasMaxLength(Test.NameMaxLength)
                .IsRequired(true);

            builder.Property(es => es.Semester)
                .HasMaxLength(Semester.NameMaxLength)
                .IsRequired(true)
                .HasConversion(v => v.ToString(), v => Enumeration.FromDisplayName<Semester>(v));

            builder.HasOne<Subject>(es => es.Subject)
                .WithMany(s => s.Tests)
                .IsRequired(false)
                .HasForeignKey($"{nameof(Subject)}Id")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
