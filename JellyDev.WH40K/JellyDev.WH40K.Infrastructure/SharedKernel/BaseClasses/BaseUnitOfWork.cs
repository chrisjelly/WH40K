using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel
{
    /// <summary>
    /// Abstract base class for units of work
    /// </summary>
    /// <typeparam name="TDbContext">Type of database context</typeparam>
    public abstract class BaseUnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Database context
        /// </summary>
        private readonly TDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">Database context</param>
        public BaseUnitOfWork(TDbContext dbContext) => _dbContext = dbContext;

        /// <summary>
        /// Commit the unit of work
        /// </summary>
        /// <returns>Task</returns>
        public async Task CommitAsync() => await _dbContext.SaveChangesAsync();

        /// <summary>
        /// Commit the virtual cards unit of work
        /// </summary>
        public void Commit() => _dbContext.SaveChanges();
    }
}
