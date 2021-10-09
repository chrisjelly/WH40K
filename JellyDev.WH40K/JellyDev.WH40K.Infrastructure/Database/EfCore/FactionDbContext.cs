using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.Database.EfCore
{
    /// <summary>
    /// Database context for the Faction aggregate
    /// </summary>
    public class FactionDbContext : BaseDbContext<FactionDbContext>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options</param>
        public FactionDbContext(DbContextOptions<FactionDbContext> options) : base(options) { }

        /// <summary>
        /// On model creating override
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FactionEntityTypeConfiguration());
        }

        /// <summary>
        /// Faction aggregate type configuration
        /// </summary>
        public class FactionEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Faction.FactionAggregate>
        {
            /// <summary>
            /// Configure
            /// </summary>
            /// <param name="builder">Builder</param>
            public void Configure(EntityTypeBuilder<FactionAggregate> builder)
            {
                // Setup table and primary key
                builder.ToTable("Factions", Constants.DB_SCHEMA);
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id)
                    .HasConversion(
                        x => x.Value,
                        x => new FactionId(x)
                    );

                // Setup columns
                builder.Property(x => x.Name)
                        .HasConversion(
                            x => x.Value,
                            x => Name.FromString(x))
                        .HasColumnName("Name");

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
