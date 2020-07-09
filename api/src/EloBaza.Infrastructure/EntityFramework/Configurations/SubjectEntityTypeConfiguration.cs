using EloBaza.Domain.Subject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class SubjectEntityTypeConfiguration : IEntityTypeConfiguration<SubjectAggregate>
    {
        public void Configure(EntityTypeBuilder<SubjectAggregate> builder)
        {
            builder.ToTable("Subject");

            builder.HasKey("_id")
                .HasName("Id");

            builder.Property("_id")
                .HasColumnName("SubjectId");

            builder.Property(s => s.Key)
                .IsRequired(true);

            builder.HasAlternateKey(s => s.Key);

            builder.Property(s => s.Name)
                .HasMaxLength(SubjectAggregate.NameMaxLength)
                .IsRequired(true);
        }
    }
}