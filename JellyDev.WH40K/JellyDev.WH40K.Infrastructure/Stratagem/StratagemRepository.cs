using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Infrastructure.Database.Contexts;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.Stratagem
{
    /// <summary>
    /// Repository for the stratagem aggregate
    /// </summary>
    public class StratagemRepository : IRepository<StratagemAggregate, StratagemId>
    {
        /// <summary>
        /// Issuer environment database set
        /// </summary>
        private readonly DbSet<StratagemAggregate> _stratagems;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">Stratagem database context</param>
        public StratagemRepository(StratagemDbContext dbContext) => _stratagems = dbContext.Set<StratagemAggregate>();

        /// <summary>
        /// Load an aggregate
        /// </summary>
        /// <param name="id">ID of the issuer environment</param>
        /// <returns>The requested issuer environment</returns>
        public StratagemAggregate Load(StratagemId id) => _stratagems.SingleOrDefault(x => x.Id == id);

        /// <summary>
        /// Add a new aggregate
        /// </summary>
        /// <param name="entity">The new aggregate</param>
        /// <returns>Task</returns>
        public async Task AddAsync(StratagemAggregate entity) => await _stratagems.AddAsync(entity);

        /// <summary>
        /// Check for the existence of an aggregate
        /// </summary>
        /// <param name="id">ID of the aggregate</param>
        /// <returns>True if the aggregate exists</returns>
        public bool Exists(StratagemId id) => _stratagems.SingleOrDefault(x => x.Id == id) != null;

        /// <summary>
        /// Update an aggregate
        /// </summary>
        /// <param name="entity">The updated aggregate</param>
        public void Update(StratagemAggregate entity) => _stratagems.Update(entity);
    }
}
