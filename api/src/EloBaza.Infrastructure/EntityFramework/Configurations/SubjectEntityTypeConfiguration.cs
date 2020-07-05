using EloBaza.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    class SubjectEntityTypeConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable(nameof(Subject));

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName($"{nameof(Subject)}{nameof(Subject.Id)}");

            builder.Property(s => s.Name)
                .HasMaxLength(Subject.NameMaxLength)
                .IsRequired(true);

            builder.HasIndex(nameof(Subject.Name))
                .IsUnique();
        }
    }
}