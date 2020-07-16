using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Queries.SubjectAggregate.ExamSession.Get
{
    public class GetExamSessionDetails : IRequest<ExamSessionDetailsReadModel>
    {
        public Guid SubjectKey { get; private set; }
        public Guid ExamSessionKey { get; private set; }

        public GetExamSessionDetails(Guid subjectKey, Guid examSessionKey)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject's key must be provided");
                validationContext.Validate(() => examSessionKey == default, nameof(examSessionKey), "Exam session's key must be provided");
            }

            SubjectKey = subjectKey;
            ExamSessionKey = examSessionKey;
        }
    }
}
