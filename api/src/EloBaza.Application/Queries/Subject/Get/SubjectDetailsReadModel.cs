using EloBaza.Application.Queries.ExamSession;
using System;
using System.Collections.Generic;

namespace EloBaza.Application.Queries.Subject.Get
{
    /// <summary>
    /// Subject details representation model
    /// </summary>
    public class SubjectDetailsReadModel
    {
        /// <summary>
        /// Subject's key
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Subject's name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// List of subject's exam sessions
        /// </summary>
        public ICollection<ExamSessionReadModel> ExamSessions { get; private set; } = new List<ExamSessionReadModel>();
    }
}