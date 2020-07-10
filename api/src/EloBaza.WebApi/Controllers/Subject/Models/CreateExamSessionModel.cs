using System.ComponentModel.DataAnnotations;

namespace EloBaza.WebApi.Controllers.Subject.Models
{
    /// <summary>
    /// Data required for creating an exam session
    /// </summary>
    public class CreateExamSessionModel
    {
        /// <summary>
        /// Exam session year - range 1950 to 2150
        /// </summary>
        [Required]
        [Range(1950, 2150)]
        public short Year { get; set; }

        /// <summary>
        /// Exam session semester - Winter, Summer
        /// </summary>
        [Required]
        [RegularExpression("(^winter$)|(^summer$)/i", ErrorMessage = "Available values - Winter, Summer")]
        public string Semester { get; set; } = default!;
    }
}
