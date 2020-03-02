using EloBaza.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    class TopicEntityTypeConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable(nameof(Topic));

            builder.HasKey(s => s.Id);

            builder.Property<string>(nameof(Topic.Name))
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName(nameof(Topic.Name))
                .IsRequired(true);

            builder.HasIndex(nameof(Topic.Name))
                .IsUnique();
        }
    }
}
