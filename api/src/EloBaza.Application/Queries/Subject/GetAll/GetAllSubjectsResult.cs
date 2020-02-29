using EloBaza.Application.Queries.Common;
using System.Collections.Generic;

namespace EloBaza.Application.Queries.Subject.GetAll
{
    /// <summary>
    /// Result containing list of subject model representations
    /// </summary>
    public class GetAllSubjectsResult : PagedResult
    {
        /// <summary>
        /// List of subject model representation
        /// </summary>
        public IEnumerable<SubjectReadModel> SubjectReadModels { get; private set; }

        public GetAllSubjectsResult(IEnumerable<SubjectReadModel> subjectReadModels, PagingInfo pagingInfo) : base(pagingInfo)
        {
            SubjectReadModels = subjectReadModels;
        }
    }
}
