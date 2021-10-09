using System.Threading.Tasks;

namespace JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces
{
    /// <summary>
    /// Unit of work interface
    /// </summary>
    public interface IUnitOfWork
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
