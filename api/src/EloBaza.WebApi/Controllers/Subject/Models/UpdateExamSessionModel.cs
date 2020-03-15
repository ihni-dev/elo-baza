using EloBaza.Domain;
using System.ComponentModel.DataAnnotations;

namespace EloBaza.WebApi.Controllers.Subject.Models
{
    /// <summary>
    /// Data required for updating an exam session
    /// </summary>
    public class UpdateExamSessionModel
    {
        /// <summary>
        /// Exam session year
        /// </summary>
        [Range(1950, 2150)]
        public int? Year { get; set; }
        /// <summary>
        /// Exam session semester
        /// </summary>
        public Semester? Semester { get; set; }
    }
}
