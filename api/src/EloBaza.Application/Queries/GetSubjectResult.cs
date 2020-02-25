namespace EloBaza.Application.Queries
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
