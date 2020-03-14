using System.ComponentModel.DataAnnotations;

namespace EloBaza.WebApi.Controllers.Subject.Models
{
    /// <summary>
    /// Data required for updating a subject
    /// </summary>
    public class UpdateSubjectModel
    {
        /// <summary>
        /// Name of the subject to update
        /// </summary>
        [MinLength(1)]
        public string? Name { get; set; }
    }
}
