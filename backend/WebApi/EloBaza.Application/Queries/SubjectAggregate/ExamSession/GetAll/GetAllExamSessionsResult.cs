using System.Collections.Generic;

namespace EloBaza.Application.Queries.SubjectAggregate.ExamSession.GetAll
{
    /// <summary>
    /// Result containing list of exam session model representations
    /// </summary>
    public class GetAllExamSessionsResult
    {
        /// <summary>
        /// List of exam session model representations
        /// </summary>
        public IEnumerable<ExamSessionReadModel> Data { get; private set; }

        public GetAllExamSessionsResult(IEnumerable<ExamSessionReadModel> data)
        {
            Data = data;
        }
    }
}
