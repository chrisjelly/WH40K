﻿using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace JellyDev.WH40K.Infrastructure.Database.EfCore
{
    /// <summary>
    /// Database context for the Stratagem aggregate
    /// </summary>
    public class StratagemDbContext : BaseDbContext<StratagemDbContext>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options</param>
        public StratagemDbContext(DbContextOptions<StratagemDbContext> options) : base(options) { }

        /// <summary>
        /// On model creating override
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StratagemEntityTypeConfiguration());
        }

        /// <summary>
        /// Stratagem aggregate type configuration
        /// </summary>
        public class StratagemEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Stratagem.StratagemAggregate>
        {
            /// <summary>
            /// Configure
            /// </summary>
            /// <param name="builder">Builder</param>
            public void Configure(EntityTypeBuilder<StratagemAggregate> builder)
            {
                // Setup table and primary key
                builder.ToTable("Stratagems", Constants.DB_SCHEMA);
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id)
                    .HasConversion(
                        x => x.Value,
                        x => new StratagemId(x)
                    );

                // Setup the mapping table of phases
                builder.OwnsMany(x => x.Phases, x =>
                {
                    x.Property<StratagemId>("StratagemId")
                        .HasColumnName("StratagemId")
                        .HasColumnType("uniqueidentifier");
                    x.Property<PhaseEnum>("Value")
                        .HasColumnName("Phase")
                        .HasColumnType("int");
                    x.Property<DateTime>("Created");
                    x.HasKey("StratagemId", "Value");
                    x.ToTable("Stratagem_Phases", Constants.DB_SCHEMA);
                    x.WithOwner()
                        .HasForeignKey("StratagemId");
                });

                // Setup columns
                builder.Property(x => x.FactionId)
                    .HasConversion(
                        x => x.Value,
                        x => new FactionId(x))
                    .IsRequired()
                    .HasColumnName("FactionId");

                builder.Property(x => x.Name)
                    .HasConversion(
                        x => x.Value,
                        x => Name.FromString(x))
                    .HasColumnName("Name");

                builder.Property(x => x.Description)
                    .HasConversion(
                        x => x.Value,
                        x => Description.FromString(x))
                    .HasColumnName("Description");

                builder.Property(x => x.CommandPoints)
                    .HasConversion(
                        x => x.Value,
                        x => Amount.FromInt(x))
                    .HasColumnName("CommandPoints");

                // Setup shadow properties
                builder.Property<DateTime>("Created");
                builder.Property<DateTime?>("LastUpdated");
            }
        }
    }
}
