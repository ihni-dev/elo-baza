namespace EloBaza.Application.Queries.SubjectAggregate.GetAll
{
    public class SubjectFilteringParameters
    {
        public string Name { get; private set; }

        public SubjectFilteringParameters(string name)
        {
            Name = name;
        }
    }
}
