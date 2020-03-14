namespace EloBaza.Application.Queries.Subject
{
    /// <summary>
    /// Subject representation model
    /// </summary>
    public class SubjectReadModel
    {
        /// <summary>
        /// Subject name
        /// </summary>
        public string Name { get; private set; }

        public SubjectReadModel(string name)
        {
            Name = name;
        }
    }
}