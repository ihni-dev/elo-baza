using EloBaza.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    class TopicEntityTypeConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("Topic");

            builder.HasKey(o => o.Id);

            builder
                .Property<string>(nameof(Subject.Name))
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName(nameof(Subject.Name))
                .IsRequired(true);
        }
    }
}
