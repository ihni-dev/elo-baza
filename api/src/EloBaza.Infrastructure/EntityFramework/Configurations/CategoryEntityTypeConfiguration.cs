using EloBaza.Domain.Subject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey("_id")
                .HasName("CategoryId");

            builder.Property("_id")
                .HasColumnName($"CategoryId");

            builder.Property(c => c.Key)
                .IsRequired(true);

            builder.HasAlternateKey(c => c.Key);

            builder.Property(c => c.Name)
                .HasMaxLength(Category.CategoryNameMaxLength)
                .IsRequired(true);

            builder.HasOne(c => c.ParentCategory)
                .WithMany(pc => pc.SubCategories)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Subject)
                .WithMany(s => s.Categories)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
