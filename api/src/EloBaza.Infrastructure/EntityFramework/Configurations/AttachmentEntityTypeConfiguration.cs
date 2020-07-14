using EloBaza.Domain.QuestionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    public class AttachmentEntityTypeConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable(nameof(Attachment));

            builder.HasKey("Id");
            builder.Property("Id")
                .HasColumnName($"{nameof(Attachment)}Id");

            builder.Property(a => a.Key)
                .HasColumnName($"{nameof(Attachment)}Key");
            builder.HasAlternateKey(a => a.Key);

            builder.Property(a => a.FileName)
                .HasMaxLength(Attachment.FileSystemMaximumFileName);

            builder.Property(a => a.FileUri)
                .HasConversion(v => v.ToString(), v => new Uri(v))
                .HasColumnType("varchar(2048)");

            builder.HasOne<Explanation>(a => a.Explanation)
                .WithMany(e => e.Attachments)
                .IsRequired(false)
                .HasForeignKey($"{nameof(Explanation)}Id")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne<Question>(a => a.Question)
                .WithMany(q => q.Attachments)
                .IsRequired(false)
                .HasForeignKey($"{nameof(Question)}Id")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
