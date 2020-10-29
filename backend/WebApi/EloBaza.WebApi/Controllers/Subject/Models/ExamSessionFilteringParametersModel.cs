namespace EloBaza.WebApi.Controllers.Subject.Models
{
    /// <summary>
    /// Parameters for filtering results by exam session properites
    /// </summary>
    public class ExamSessionFilteringParametersModel
    {
        /// <summary>
        /// Name of the exam session to filter by
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
