using EloBaza.Domain.Subject;

namespace EloBaza.Application.Queries.ExamSession
{
    /// <summary>
    /// Exam session representation model
    /// </summary>
    public class ExamSessionReadModel
    {
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
        public Semester? Semester { get; set; }
    }
}