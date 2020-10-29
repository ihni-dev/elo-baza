namespace EloBaza.Application.Queries.SubjectAggregate.Category.GetAll
{
    public class CategoryFilteringParameters
    {
        public string Name { get; private set; }

        public CategoryFilteringParameters(string name)
        {
            Name = name;
        }
    }
}
