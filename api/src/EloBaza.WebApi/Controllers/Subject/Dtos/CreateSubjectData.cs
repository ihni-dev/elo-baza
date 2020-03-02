using EloBaza.Application.Commands.Create;
using System.ComponentModel.DataAnnotations;

namespace EloBaza.WebApi.Controllers.Subject.Dtos
{
    /// <summary>
    /// Data required for creating a subject
    /// </summary>
    public class CreateSubjectData : ICreateSubjectData
    {
        /// <summary>
        /// Name of the subject
        /// </summary>
        [Required]
        [MinLength(1)]
        public string Name { get; set; } = null!;
    }
}
