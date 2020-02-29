namespace EloBaza.Application.Queries.Subject.Get
{
    /// <summary>
    /// Result containing single subject model reporesentation
    /// </summary>
    public class GetSubjectResult
    {
        /// <summary>
        /// Subject model representation
        /// </summary>
        public SubjectReadModel SubjectReadModel { get; set; }

        public GetSubjectResult(SubjectReadModel subjectReadModel)
        {
            SubjectReadModel = subjectReadModel;
        }
    }
}
