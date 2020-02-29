using EloBaza.Application.Commands.Create;
using System.ComponentModel.DataAnnotations;

namespace EloBaza.WebApi.Controllers.Subject.Dtos
{
    /// <summary>
    /// Create subject DTO
    /// </summary>
    public class CreateSubjectDto : ICreateSubjectData
    {
        /// <summary>
        /// Name of the subject
        /// </summary>
        [Required]
        [MinLength(1)]
        public string Name { get; set; } = null!;
    }
}
