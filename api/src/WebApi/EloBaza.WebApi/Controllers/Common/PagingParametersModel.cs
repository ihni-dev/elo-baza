using System.ComponentModel.DataAnnotations;

namespace EloBaza.WebApi.Controllers.Common
{
    /// <summary>
    /// Parameters for specifying page number and number of maximum records 
    /// </summary>
    public class PagingParametersModel
    {
        /// <summary>
        /// Page number (default 1)
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Maximum number of records (default 30)
        /// </summary>
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 30;
    }
}
