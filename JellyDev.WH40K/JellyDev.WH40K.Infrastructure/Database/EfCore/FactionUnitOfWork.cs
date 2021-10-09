using JellyDev.WH40K.Infrastructure.SharedKernel;


namespace JellyDev.WH40K.Infrastructure.Database.EfCore
{
    /// <summary>
    /// Unit of work for the faction aggregate 
    /// </summary>
    public class FactionUnitOfWork : BaseUnitOfWork<FactionDbContext>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">Faction database context</param>
        public FactionUnitOfWork(FactionDbContext dbContext) : base(dbContext)
        {

        }
    }
}
