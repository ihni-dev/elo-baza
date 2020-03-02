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
        public SubjectReadModel Data { get; set; }

        public GetSubjectResult(SubjectReadModel data)
        {
            Data = data;
        }
    }
}
