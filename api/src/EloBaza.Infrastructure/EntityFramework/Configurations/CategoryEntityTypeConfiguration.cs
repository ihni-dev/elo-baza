using EloBaza.Domain.SubjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));

            builder.HasKey("Id");
            builder.Property("Id")
                .HasColumnName($"{nameof(Category)}Id");

            builder.Property(c => c.Key)
                .HasColumnName($"{nameof(Category)}Key");
            builder.HasAlternateKey(c => c.Key);

            builder.Property(c => c.Name)
                .HasMaxLength(Category.CategoryNameMaxLength)
                .IsRequired(true);

            builder.HasOne<Category>(c => c.ParentCategory)
                .WithMany(pc => pc.SubCategories)
                .IsRequired(false)
                .HasForeignKey($"Parent{nameof(Category)}Id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Subject>(c => c.Subject)
                .WithMany(s => s.Categories)
                .IsRequired(false)
                .HasForeignKey($"{nameof(Subject)}Id")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
