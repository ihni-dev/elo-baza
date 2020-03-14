namespace EloBaza.WebApi.Controllers.Subject.Models
{
    /// <summary>
    /// Parameters for filtering results by subject properites
    /// </summary>
    public class SubjectFilteringParametersModel
    {
        /// <summary>
        /// Name of the subject to filter by
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
