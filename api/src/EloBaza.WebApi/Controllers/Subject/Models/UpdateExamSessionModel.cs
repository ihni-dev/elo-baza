using System.ComponentModel.DataAnnotations;

namespace EloBaza.WebApi.Controllers.Subject.Models
{
    /// <summary>
    /// Data required for updating an exam session
    /// </summary>
    public class UpdateExamSessionModel
    {
        /// <summary>
        /// Exam session year (optional) - range 1950 to 2150
        /// </summary>
        [Range(1950, 2150)]
        public short? Year { get; set; }

        /// <summary>
        /// Exam session semester (optional) - Winter, Summer
        /// </summary>
        [MinLength(1)]
        public string? Semester { get; set; }

        /// <summary>
        /// Exam session's resit number (optional) - range 1 to 10
        /// </summary>
        [Required]
        [Range(1, 10)]
        public byte? ResitNumber { get; set; }
    }
}
