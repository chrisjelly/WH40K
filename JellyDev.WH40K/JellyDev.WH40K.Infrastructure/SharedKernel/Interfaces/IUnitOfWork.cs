using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces
{
    /// <summary>
    /// Unit of work interface
    /// </summary>
    /// <typeparam name="TDbContext">Type of database context</typeparam>
    public interface IUnitOfWork<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Commit the unit of work
        /// </summary>
        /// <returns>Task</returns>
        Task CommitAsync();

        /// <summary>
        /// Commit the unit of work
        /// </summary>
        void Commit();
    }
}
