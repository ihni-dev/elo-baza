using EloBaza.Application.Queries.ExamSession;
using System.Collections.Generic;

namespace EloBaza.Application.Queries.Subject.Get
{
    /// <summary>
    /// Subject details representation model
    /// </summary>
    public class SubjectDetailsReadModel
    {
        /// <summary>
        /// Subject name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// List of subject exam sessions
        /// </summary>
        public ICollection<ExamSessionReadModel>? ExamSessions { get; set; }
    }
}