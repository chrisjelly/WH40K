using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.Database.Contexts
{
    /// <summary>
    /// Database context for the Stratagem aggregate
    /// </summary>
    public class StratagemDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options</param>
        public StratagemDbContext(DbContextOptions<StratagemDbContext> options) : base(options) { }

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
                builder.OwnsMany(x => x.Phases.Select(x => x.ToString()).ToArray(), x =>
                {
                    x.Property<StratagemId>("StratagemId")
                        .HasColumnName("StratagemId")
                        .HasColumnType("uniqueidentifier");
                    x.Property<Guid>("Phase")
                        .HasColumnName("Phase")
                        .HasColumnType("nvarchar(20)");
                    x.Property<DateTime>("Created");
                    x.HasKey("StratagemId", "Phase");
                    x.ToTable("Stratagem_Phases", Constants.DB_SCHEMA);
                    x.WithOwner()
                        .HasForeignKey("StratagemId");
                });

                // Setup columns
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

                // Setup shadow properties
                builder.Property<DateTime>("Created");
                builder.Property<DateTime?>("LastUpdated");
            }
        }

        /// <summary>
        /// Save changes override
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Metadata.FindProperty("Created") != null)
                    {
                        entry.Property("Created").CurrentValue = DateTime.UtcNow;
                    }
                }
                if (entry.State == EntityState.Modified)
                {
                    if (entry.Metadata.FindProperty("LastUpdated") != null)
                    {
                        entry.Property("LastUpdated").CurrentValue = DateTime.UtcNow;
                    }
                }
            }
            return await base.SaveChangesAsync();
        }
    }
}
