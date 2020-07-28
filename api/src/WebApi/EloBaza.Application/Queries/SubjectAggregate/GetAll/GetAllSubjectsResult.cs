using EloBaza.Application.Queries.Common;
using System.Collections.Generic;

namespace EloBaza.Application.Queries.SubjectAggregate.GetAll
{
    /// <summary>
    /// Result containing list of subject model representations
    /// </summary>
    public class GetAllSubjectsResult : PagedResult
    {
        /// <summary>
        /// List of subject model representations
        /// </summary>
        public IEnumerable<SubjectReadModel> Data { get; private set; }

        public GetAllSubjectsResult(IEnumerable<SubjectReadModel> data, PagingInfo pagingInfo) : base(pagingInfo)
        {
            Data = data;
        }
    }
}
