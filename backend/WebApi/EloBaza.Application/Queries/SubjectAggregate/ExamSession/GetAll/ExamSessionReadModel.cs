using System;

namespace EloBaza.Application.Queries.SubjectAggregate.ExamSession.GetAll
{
    /// <summary>
    /// Exam session representation model
    /// </summary>
    public class ExamSessionReadModel
    {
        /// <summary>
        /// Exam session key
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Exam session name
        /// </summary>
        public string? Name { get; set; }
    }
}
