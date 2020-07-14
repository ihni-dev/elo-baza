using EloBaza.Domain.SubjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class SubjectEntityTypeConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable(nameof(Subject));

            builder.HasKey("Id");
            builder.Property("Id")
                .HasColumnName($"{nameof(Subject)}Id");

            builder.Property(s => s.Key)
                .HasColumnName($"{nameof(Subject)}Key");
            builder.HasAlternateKey(s => s.Key);

            builder.Property(s => s.Name)
                .HasMaxLength(Subject.NameMaxLength)
                .IsRequired(true);
        }
    }
}