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
        public short? Year { get; set; }

        /// <summary>
        /// Exam session semester - Winter, Summer
        /// </summary>
        [RegularExpression("(^winter$)|(^summer$)/i", ErrorMessage = "Available values - Winter, Summer")]
        public string? Semester { get; set; }
    }
}
