using EloBaza.Application.Queries.SubjectAggregate.ExamSession;
using System;
using System.Collections.Generic;

namespace EloBaza.Application.Queries.SubjectAggregate.Get
{
    /// <summary>
    /// Subject details representation model
    /// </summary>
    public class SubjectDetailsReadModel
    {
        /// <summary>
        /// Subject key
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Subject name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// List of subject exam sessions
        /// </summary>
        public ICollection<ExamSessionReadModel> ExamSessions { get; private set; } = new List<ExamSessionReadModel>();
    }
}