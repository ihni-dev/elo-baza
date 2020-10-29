using EloBaza.Application.Queries.Common;
using System.Collections.Generic;

namespace EloBaza.Application.Queries.SubjectAggregate.Category.GetAll
{
    /// <summary>
    /// Result containing list of category model representations
    /// </summary>
    public class GetAllCategoriesResult : PagedResult
    {
        /// <summary>
        /// List of category model representations
        /// </summary>
        public IEnumerable<CategoryReadModel> Data { get; private set; }

        public GetAllCategoriesResult(IEnumerable<CategoryReadModel> data, PagingInfo pagingInfo) : base(pagingInfo)
        {
            Data = data;
        }
    }
}
