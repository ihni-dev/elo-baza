namespace EloBaza.Application.Queries.ExamSession
{
    /// <summary>
    /// Exam session representation model
    /// </summary>
    public class ExamSessionReadModel
    {
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