using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Infrastructure.Database.EfCore;
using JellyDev.WH40K.Infrastructure.SharedKernel;

namespace JellyDev.WH40K.Infrastructure.Faction
{
    /// <summary>
    /// Repository for the faction aggregate
    /// </summary>
    public class FactionRepository : BaseRepository<FactionDbContext, FactionAggregate, FactionId>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">Faction database context</param>
        public FactionRepository(FactionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
