﻿// <auto-generated />
using System;
using EloBaza.Infrastructure.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EloBaza.MigrationTool.Migrations
{
    [DbContext(typeof(SubjectDbContext))]
    [Migration("20200302192245_CrateTopicTableAndMakeSubjectRelation")]
    partial class CrateTopicTableAndMakeSubjectRelation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EloBaza.Domain.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("EloBaza.Domain.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("SubjectId");

                    b.ToTable("Topic");
                });

            modelBuilder.Entity("EloBaza.Domain.Topic", b =>
                {
                    b.HasOne("EloBaza.Domain.Subject", null)
                        .WithMany("Topics")
                        .HasForeignKey("SubjectId");
                });
#pragma warning restore 612, 618
        }
    }
}