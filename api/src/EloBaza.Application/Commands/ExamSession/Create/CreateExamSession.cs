﻿using EloBaza.Application.Queries.ExamSession;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.ExamSession.Create
{
    public class CreateExamSession : IRequest<ExamSessionReadModel>
    {
        public Guid SubjectKey { get; private set; }
        public CreateExamSessionData Data { get; private set; }

        public CreateExamSession(Guid subjectKey, CreateExamSessionData data)
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
