using EloBaza.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    class AnswerEntityTypeConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable(nameof(Answer));

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName($"{nameof(Answer)}{nameof(Answer.Id)}");

            builder.Property(a => a.Content)
                .IsRequired(true);

            builder.Property(a => a.IsValid)
                .IsRequired(true);

            builder.HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
