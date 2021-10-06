
namespace JellyDev.WH40K.Infrastructure.SharedKernel
{
    /// <summary>
    /// Static helper class for queries
    /// </summary>
    public static class QueriesHelper
    {
        /// <summary>
        /// Calculate offset for paging
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Offset value</returns>
        public static int Offset(int page, int pageSize) => (page - 1) * pageSize;
    }
}
