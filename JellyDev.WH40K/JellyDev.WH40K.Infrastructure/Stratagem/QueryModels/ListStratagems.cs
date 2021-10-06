

namespace JellyDev.WH40K.Infrastructure.Stratagem.QueryModels
{
    /// <summary>
    /// Query model to get a list of stratagems
    /// </summary>
    public class ListStratagems
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
