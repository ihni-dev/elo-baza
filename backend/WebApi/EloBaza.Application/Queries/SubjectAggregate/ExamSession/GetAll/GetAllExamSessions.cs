using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Queries.SubjectAggregate.ExamSession.GetAll
{
    public class GetAllExamSessions : IRequest<GetAllExamSessionsResult>
    {
        public Guid SubjectKey { get; private set; }

        public GetAllExamSessions(Guid subjectKey)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject key must be provided");
            }

            SubjectKey = subjectKey;
        }
    }
}
