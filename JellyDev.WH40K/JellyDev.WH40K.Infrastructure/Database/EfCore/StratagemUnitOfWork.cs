using JellyDev.WH40K.Infrastructure.SharedKernel;

namespace JellyDev.WH40K.Infrastructure.Database.EfCore
{
    /// <summary>
    /// Unit of work for the stratagem aggregate 
    /// </summary>
    public class StratagemUnitOfWork : BaseUnitOfWork<StratagemDbContext>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">Stratagem database context</param>
        public StratagemUnitOfWork(StratagemDbContext dbContext) : base(dbContext)
        {

        }
    }
}
