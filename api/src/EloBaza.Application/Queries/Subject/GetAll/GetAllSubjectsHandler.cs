using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Queries.Subject.GetAll
{
    class GetAllSubjectsHandler : IRequestHandler<GetAllSubjects, GetAllSubjectsResult>
    {
        public Task<GetAllSubjectsResult> Handle(GetAllSubjects request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
