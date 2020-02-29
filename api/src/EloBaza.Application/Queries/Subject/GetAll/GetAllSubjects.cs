using EloBaza.Application.Queries.Common;
using MediatR;

namespace EloBaza.Application.Queries.Subject.GetAll
{
    public class GetAllSubjects : PagedQuery, IRequest<GetAllSubjectsResult>
    {


        public GetAllSubjects(int skip, int take) : base(skip, take)
        {

        }
    }
}
