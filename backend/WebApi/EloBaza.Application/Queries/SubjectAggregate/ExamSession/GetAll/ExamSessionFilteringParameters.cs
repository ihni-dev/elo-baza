namespace EloBaza.Application.Queries.SubjectAggregate.ExamSession.GetAll
{
    public class ExamSessionFilteringParameters
    {
        public string Name { get; private set; }

        public ExamSessionFilteringParameters(string name)
        {
            Name = name;
        }
    }
}
