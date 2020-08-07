using EloBaza.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasKey("Id");
            builder.Property("Id")
                .HasColumnName($"{nameof(User)}Id");

            builder.Property(s => s.Key)
                .HasColumnName($"{nameof(User)}Key");
            builder.HasAlternateKey(s => s.Key);

            builder.Property(s => s.Email)
                .HasMaxLength(User.EmailMaxLength)
                .IsRequired(true);

            builder.Property(s => s.DisplayName)
                .HasMaxLength(User.DisplayNameMaxLength)
                .IsRequired(true);
        }
    }
}
