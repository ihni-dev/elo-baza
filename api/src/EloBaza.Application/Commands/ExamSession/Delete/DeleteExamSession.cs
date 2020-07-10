using EloBaza.Application.Commands.Common;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.ExamSession.Delete
{
    public class DeleteExamSession : AuditableCommand, IRequest
    {
        public Guid SubjectKey { get; private set; }
        public Guid ExamSessionKey { get; private set; }

        public DeleteExamSession(Guid subjectKey, Guid examSessionKey, int requestorId) : base(requestorId)
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
