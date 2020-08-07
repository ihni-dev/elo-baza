using System.ComponentModel.DataAnnotations;

namespace EloBaza.WebApi.Controllers.Subject.Models
{
    /// <summary>
    /// Data required for creating an exam session
    /// </summary>
    public class CreateExamSessionModel
    {
        /// <summary>
        /// Exam session's year - range 1950 to 2150
        /// </summary>
        [Required]
        [Range(1950, 2150)]
        public short Year { get; set; }

        /// <summary>
        /// Exam session's semester - Winter, Summer
        /// </summary>
        [Required]
        [MinLength(1)]
        public string Semester { get; set; } = default!;

        /// <summary>
        /// Exam session's resit number (optional) - range 1 to 10
        /// </summary>
        [Required]
        [Range(1, 10)]
        public byte ResitNumber { get; set; } = 0;
    }
}
