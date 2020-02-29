using EloBaza.Application.Queries.Subject;

namespace EloBaza.WebApi.Controllers.Subject
{
    /// <summary>
    /// Parameters for filtering results by subject properites
    /// </summary>
    public class SubjectFilteringParameters : ISubjectFilteringParameters
    {
        /// <summary>
        /// Name of the subject to filter by
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
