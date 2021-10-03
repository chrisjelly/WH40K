using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Infrastructure.Database.EfCore;
using JellyDev.WH40K.Infrastructure.SharedKernel;

namespace JellyDev.WH40K.Infrastructure.Stratagem
{
    /// <summary>
    /// Repository for the stratagem aggregate
    /// </summary>
    public class StratagemRepository : BaseRepository<StratagemDbContext, StratagemAggregate, StratagemId>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">Stratagem database context</param>
        public StratagemRepository(StratagemDbContext dbContext) : base(dbContext)
        {

        }
    }
}
