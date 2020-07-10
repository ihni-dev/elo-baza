using EloBaza.Domain.SubjectAggregate;
using System;

namespace EloBaza.Application.Queries.ExamSession.Get
{
    /// <summary>
    /// Exam session representation model
    /// </summary>
    public class ExamSessionDetailsReadModel
    {
        /// <summary>
        /// Exam session's key
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Exam session's name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Exam session's year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Exam session's semester
        /// </summary>
        public string? Semester { get; set; }
    }
}
