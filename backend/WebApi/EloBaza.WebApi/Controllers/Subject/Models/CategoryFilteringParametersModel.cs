namespace EloBaza.WebApi.Controllers.Subject.Models
{
    /// <summary>
    /// Parameters for filtering results by category properites
    /// </summary>
    public class CategoryFilteringParametersModel
    {
        /// <summary>
        /// Name of the category to filter by
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
