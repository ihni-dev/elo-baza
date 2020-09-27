using System.ComponentModel.DataAnnotations;

namespace EloBaza.WebApi.Controllers.Subject.Models
{
    /// <summary>
    /// Data required for updating a subject
    /// </summary>
    public class UpdateSubjectModel
    {
        /// <summary>
        /// Name of the subject
        /// </summary>
        [Required]
        [MinLength(1)]
        [MaxLength(Domain.SubjectAggregate.Subject.NameMaxLength)]
        public string Name { get; set; } = default!;
    }
}
