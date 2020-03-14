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

            builder.Property<string>(nameof(Subject.Name))
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName(nameof(Subject.Name))
                .IsRequired(true);

            builder.HasIndex(nameof(Subject.Name))
                .IsUnique();

            builder.HasMany(s => s.ExamSessions)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}