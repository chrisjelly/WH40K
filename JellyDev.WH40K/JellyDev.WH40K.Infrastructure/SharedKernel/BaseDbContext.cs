using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel
{
    /// <summary>
    /// Abstract base class for DbContext classes
    /// </summary>
    public abstract class BaseDbContext<T> : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options</param>
        public BaseDbContext(DbContextOptions options) : base(options) { }

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
