

namespace JellyDev.WH40K.Infrastructure.SharedKernel
{
    /// <summary>
    /// Abstract base class for executing list queries
    /// </summary>
    public abstract class ListQueryModel
    {
        /// <summary>
        /// Page
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
    }
}
