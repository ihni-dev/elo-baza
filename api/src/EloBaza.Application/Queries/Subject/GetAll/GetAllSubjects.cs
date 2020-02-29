using EloBaza.Application.Queries.Common;
using MediatR;

namespace EloBaza.Application.Queries.Subject.GetAll
{
    public class GetAllSubjects : PagedQuery, IRequest<GetAllSubjectsResult>
    {
        public ISubjectFilteringParameters SubjectFilteringParameters { get; private set; }

        public GetAllSubjects(ISubjectFilteringParameters subjectFilteringParameters, IPagingParameters pagingParameters) 
            : base(pagingParameters)
        {
            SubjectFilteringParameters = subjectFilteringParameters;
        }
    }
}
