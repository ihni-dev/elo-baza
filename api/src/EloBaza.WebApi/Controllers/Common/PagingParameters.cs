using EloBaza.Application.Queries.Common;

namespace EloBaza.WebApi.Controllers.Common
{
    /// <summary>
    /// Parameters for specifying page number and number of maximum records 
    /// </summary>
    public class PagingParameters : IPagingParameters
    {
        /// <summary>
        /// Zero based page number
        /// </summary>
        public int PageIndex { get; set; } = 0;

        /// <summary>
        /// Maximum number of records
        /// </summary>
        public int PageSize { get; set; } = 50;
    }
}
