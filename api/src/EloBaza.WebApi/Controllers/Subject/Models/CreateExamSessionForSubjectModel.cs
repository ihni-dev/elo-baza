using EloBaza.Domain;
using System.ComponentModel.DataAnnotations;

namespace EloBaza.WebApi.Controllers.Subject.Models
{
    /// <summary>
    /// Data required for creating an exam session
    /// </summary>
    public class CreateExamSessionForSubjectModel
    {
        /// <summary>
        /// Exams year
        /// </summary>
        [Required]
        [Range(1950, 2150)]
        public int Year { get; set; }
        /// <summary>
        /// Exams semester
        /// </summary>
        public Semester Semester { get; set; }
    }
}
