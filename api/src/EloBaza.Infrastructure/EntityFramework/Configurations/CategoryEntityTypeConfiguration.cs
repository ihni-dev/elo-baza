using EloBaza.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName($"{nameof(Category)}{nameof(Category.Id)}");

            builder.Property(c => c.Name)
                .HasMaxLength(Category.CategoryNameMaxLength)
                .IsRequired(true);

            builder.HasOne(c => c.ParentCategory)
                .WithMany(pc => pc.SubCategories)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Subject)
                .WithMany(s => s.Categories)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
