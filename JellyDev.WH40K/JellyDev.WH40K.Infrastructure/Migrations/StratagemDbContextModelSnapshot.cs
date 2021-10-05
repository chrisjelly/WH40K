﻿// <auto-generated />
using System;
using JellyDev.WH40K.Infrastructure.Database.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JellyDev.WH40K.Infrastructure.Migrations
{
    [DbContext(typeof(StratagemDbContext))]
    partial class StratagemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JellyDev.WH40K.Domain.Stratagem.StratagemAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Stratagems", "WH");
                });

            modelBuilder.Entity("JellyDev.WH40K.Domain.Stratagem.StratagemAggregate", b =>
                {
                    b.OwnsMany("JellyDev.WH40K.Domain.SharedKernel.ValueObjects.Phase", "Phases", b1 =>
                        {
                            b1.Property<Guid>("StratagemId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("StratagemId");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("Phase");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime2");

                            b1.HasKey("StratagemId", "Value");

                            b1.ToTable("Stratagem_Phases", "WH");

                            b1.WithOwner()
                                .HasForeignKey("StratagemId");
                        });

                    b.Navigation("Phases");
                });
#pragma warning restore 612, 618
        }
    }
}
