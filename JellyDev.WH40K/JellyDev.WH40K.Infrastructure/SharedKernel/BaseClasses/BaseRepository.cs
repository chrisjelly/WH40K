using JellyDev.WH40K.Domain.SharedKernel;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel
{
    /// <summary>
    /// Abstract base class for repositories
    /// </summary>
    /// <typeparam name="TDbContext">Type of database context</typeparam>
    /// <typeparam name="TAggregate">Type of aggregate</typeparam>
    /// <typeparam name="TId">Type of aggregate ID</typeparam>
    public abstract class BaseRepository<TDbContext, TAggregate, TId> : IRepository<TAggregate, TId>
        where TDbContext : DbContext
        where TAggregate : AggregateRoot<TId>
        where TId : class
    {
        /// <summary>
        /// Database set
        /// </summary>
        private readonly DbSet<TAggregate> _dbset;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">Database context</param>
        public BaseRepository(TDbContext dbContext) => _dbset = dbContext.Set<TAggregate>();

        /// <summary>
        /// Load an aggregate
        /// </summary>
        /// <param name="id">ID of the stratagem</param>
        /// <returns>The requested stratagem</returns>
        public TAggregate Load(TId id) => _dbset.SingleOrDefault(x => x.Id == id);

        /// <summary>
        /// Add a new aggregate
        /// </summary>
        /// <param name="entity">The new aggregate</param>
        /// <returns>Task</returns>
        public async Task AddAsync(TAggregate entity) => await _dbset.AddAsync(entity);

        /// <summary>
        /// Check for the existence of an aggregate
        /// </summary>
        /// <param name="id">ID of the aggregate</param>
        /// <returns>True if the aggregate exists</returns>
        public bool Exists(TId id) => _dbset.SingleOrDefault(x => x.Id == id) != null;

        /// <summary>
        /// Update an aggregate
        /// </summary>
        /// <param name="entity">The updated aggregate</param>
        public void Update(TAggregate entity) => _dbset.Update(entity);

        /// <summary>
        /// Delete an aggregate
        /// </summary>
        /// <param name="entity">The aggregate to delete</param>
        public void Delete(TAggregate entity) => _dbset.Remove(entity);
    }
}
