using EloBaza.Application.Commands.Common;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.ExamSession.Update
{
    public class UpdateExamSession : AuditableCommand, IRequest
    {
        public Guid SubjectKey { get; private set; }
        public Guid ExamSessionKey { get; private set; }
        public UpdateExamSessionData Data { get; private set; }

        public UpdateExamSession(Guid subjectKey, Guid examSessionKey, UpdateExamSessionData data, int requestorId) : base(requestorId)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject's key must be provided");
                validationContext.Validate(() => examSessionKey == default, nameof(examSessionKey), "Exam session's key must be provided");
            }

            SubjectKey = subjectKey;
            ExamSessionKey = examSessionKey;
            Data = data;
        }
    }
}
