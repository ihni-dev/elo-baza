using System;

namespace EloBaza.Application.Queries.SubjectAggregate.ExamSession
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

        /// <summary>
        /// Exam session year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Exam session semester
        /// </summary>
        public string? Semester { get; set; }
    }
}