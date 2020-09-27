using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Queries.SubjectAggregate.Get
{
    public class GetSubjectDetails : IRequest<SubjectDetailsReadModel>
    {
        public Guid SubjectKey { get; private set; }

        public GetSubjectDetails(Guid subjectKey)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject key must be provided");
            }

            SubjectKey = subjectKey;
        }
    }
}
