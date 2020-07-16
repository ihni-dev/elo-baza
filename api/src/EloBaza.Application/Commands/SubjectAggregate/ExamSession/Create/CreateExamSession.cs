using EloBaza.Application.Commands.Common;
using EloBaza.Application.Queries.SubjectAggregate.ExamSession.Get;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.SubjectAggregate.ExamSession.Create
{
    public class CreateExamSession : AuditableCommand, IRequest<ExamSessionDetailsReadModel>
    {
        public Guid SubjectKey { get; private set; }
        public CreateExamSessionData Data { get; private set; }

        public CreateExamSession(int requestorId, Guid subjectKey, CreateExamSessionData data) : base(requestorId)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject Key must be provided");
            }

            SubjectKey = subjectKey;
            Data = data;
        }
    }
}
