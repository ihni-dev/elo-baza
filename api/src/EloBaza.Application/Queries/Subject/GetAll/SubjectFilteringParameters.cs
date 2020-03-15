namespace EloBaza.Application.Queries.Subject.GetAll
{
    public class SubjectFilteringParameters
    {
        public string? Name { get; private set; }

        public SubjectFilteringParameters(string? name)
        {
            Name = name;
        }
    }
}
