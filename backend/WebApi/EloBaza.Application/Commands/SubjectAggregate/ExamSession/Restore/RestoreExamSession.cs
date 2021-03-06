﻿using EloBaza.Application.Commands.Common;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.SubjectAggregate.ExamSession.Restore
{
    public class RestoreExamSession : AuditableCommand, IRequest
    {
        public Guid SubjectKey { get; private set; }
        public Guid ExamSessionKey { get; private set; }

        public RestoreExamSession(int requestorId, Guid subjectKey, Guid examSessionKey) : base(requestorId)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject key must be provided");
                validationContext.Validate(() => examSessionKey == default, nameof(examSessionKey), "Exam session key must be provided");
            }

            SubjectKey = subjectKey;
            ExamSessionKey = examSessionKey;
        }
    }
}
