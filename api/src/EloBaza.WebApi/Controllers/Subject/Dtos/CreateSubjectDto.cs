using EloBaza.Application.Commands.Create;

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
        public string Name { get; set; }
    }
}
