namespace EloBaza.Application.Queries.Subject.Get
{
    public class GetSubjectResult
    {
        public SubjectReadModel SubjectReadModel { get; set; }

        public GetSubjectResult(SubjectReadModel subjectReadModel)
        {
            SubjectReadModel = subjectReadModel;
        }
    }
}
