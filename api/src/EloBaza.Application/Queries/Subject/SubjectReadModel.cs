namespace EloBaza.Application.Queries.Subject
{
    public class SubjectReadModel
    {
        public string Name { get; set; }

        public SubjectReadModel(string name)
        {
            Name = name;
        }
    }
}