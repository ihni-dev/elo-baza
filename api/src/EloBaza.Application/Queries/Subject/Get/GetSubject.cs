using EloBaza.Domain.SharedKernel;
using MediatR;
using System;

namespace EloBaza.Application.Queries.Subject.Get
{
    public class GetSubject : IRequest<GetSubjectResult>
    {
        public Guid Id { get; private set; }

        public GetSubject(Guid id)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => id == default, nameof(Id), "Not empty GUID must be provided");
            }

            Id = id;
        }
    }
}
