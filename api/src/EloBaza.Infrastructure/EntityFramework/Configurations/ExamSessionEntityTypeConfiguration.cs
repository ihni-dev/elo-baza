﻿using EloBaza.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EloBaza.Infrastructure.EntityFramework.Configurations
{
    class ExamSessionEntityTypeConfiguration : IEntityTypeConfiguration<ExamSession>
    {
        public void Configure(EntityTypeBuilder<ExamSession> builder)
        {
            builder.ToTable(nameof(ExamSession));

            builder.HasKey(s => s.Id);

            builder.Property<string>(nameof(ExamSession.SubjectName))
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName(nameof(ExamSession.SubjectName))
                .IsRequired(true);

            builder.Property<string>(nameof(ExamSession.Year))
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName(nameof(ExamSession.Year))
                .IsRequired(true);

            builder.Property<Semester>(nameof(ExamSession.Semester))
                .UsePropertyAccessMode(PropertyAccessMode.Property)
                .HasColumnName(nameof(ExamSession.Semester))
                .IsRequired(true)
                .HasConversion(
                    s => s.ToString(),
                    s => (Semester)Enum.Parse(typeof(Semester), s));
        }
    }
}