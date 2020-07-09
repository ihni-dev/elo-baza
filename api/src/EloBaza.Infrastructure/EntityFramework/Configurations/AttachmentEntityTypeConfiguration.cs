using EloBaza.Domain.Question;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class AttachmentEntityTypeConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("Attachment");

            builder.HasKey("_id")
                .HasName("Id");

            builder.Property("_id")
                .HasColumnName($"AttachmentId");

            builder.Property(a => a.Key)
                .IsRequired(true);

            builder.HasAlternateKey(a => a.Key);

            builder.Property(a => a.FileName)
                .HasMaxLength(Attachment.FileSystemMaximumFileName)
                .IsRequired(true);

            builder.Property(a => a.FileUri)
                .HasConversion(v => v.ToString(), v => new Uri(v))
                .HasColumnType("varchar(2048)")
                .IsRequired(true);

            builder.Property(a => a.FileSize)
                .IsRequired(true);

            builder.HasOne(a => a.Explanation)
                .WithMany(e => e.Attachments)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(a => a.Question)
                .WithOne(q => q.Attachment)
                .HasForeignKey<Attachment>("QuestionId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
