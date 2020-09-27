using EloBaza.Application.Commands.Common;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.SubjectAggregate.Update
{
    public class UpdateSubject : AuditableCommand, IRequest
    {
        public Guid SubjectKey { get; private set; }
        public UpdateSubjectData Data { get; private set; }

        public UpdateSubject(int requestorId, Guid subjectKey, UpdateSubjectData data) : base(requestorId)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject key must be provided");
            }

            SubjectKey = subjectKey;
            Data = data;
        }
    }
}
