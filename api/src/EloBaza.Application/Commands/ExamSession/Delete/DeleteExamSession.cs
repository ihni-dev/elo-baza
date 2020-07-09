using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.ExamSession.Delete
{
    public class DeleteExamSession : IRequest
    {
        public Guid SubjectKey { get; private set; }
        public Guid ExamSessionKey { get; private set; }

        public DeleteExamSession(Guid subjectKey, Guid examSessionKey)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject Key must be provided");
                validationContext.Validate(() => examSessionKey == default, nameof(examSessionKey), "Exam session Key must be provided");
            }

            SubjectKey = subjectKey;
            ExamSessionKey = examSessionKey;
        }
    }
}
