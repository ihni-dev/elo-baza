using EloBaza.Application.Queries.Common;
using MediatR;

namespace EloBaza.Application.Queries.SubjectAggregate.GetAll
{
    public class GetAllSubjects : PagedQuery, IRequest<GetAllSubjectsResult>
    {
        public SubjectFilteringParameters SubjectFilteringParameters { get; private set; }

        public GetAllSubjects(SubjectFilteringParameters subjectFilteringParameters, PagingParameters pagingParameters)
            : base(pagingParameters)
        {
            SubjectFilteringParameters = subjectFilteringParameters;
        }
    }
}
