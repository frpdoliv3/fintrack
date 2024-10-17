﻿// <auto-generated />
using FinTrack.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinTrack.Persistence.Migrations
{
    [DbContext(typeof(FinTrackDbContext))]
    partial class FinTrackDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FinTrack.Domain.Entities.Country", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Alpha2Code")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nchar(2)")
                        .IsFixedLength();

                    b.Property<string>("Alpha3Code")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nchar(3)")
                        .IsFixedLength();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NumericCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Alpha2Code")
                        .IsUnique();

                    b.HasIndex("Alpha3Code")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Countries");
                });
#pragma warning restore 612, 618
        }
    }
}
