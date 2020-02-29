using MediatR;
using System;

namespace EloBaza.Application.Queries.Subject.Get
{
    public class GetSubject : IRequest<GetSubjectResult>
    {
        public Guid Id { get; private set; }

        public GetSubject(Guid id)
        {
            Id = id;
        }
    }
}
