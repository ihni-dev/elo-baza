using EloBaza.Domain;

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
        public string? Name { get => $"{SubjectName}-{Year}-{Semester}"; }
        /// <summary>
        /// Subject name
        /// </summary>
        public string? SubjectName { get; set; }
        /// <summary>
        /// Exam session year
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Exam session semester
        /// </summary>
        public Semester Semester { get; set; }
    }
}