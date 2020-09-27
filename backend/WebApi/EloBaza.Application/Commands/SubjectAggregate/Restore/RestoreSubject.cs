using EloBaza.Application.Commands.Common;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.SubjectAggregate.Restore
{
    public class RestoreSubject : AuditableCommand, IRequest
    {
        public Guid SubjectKey { get; private set; }

        public RestoreSubject(int requestorId, Guid subjectKey) : base(requestorId)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject key must be provided");
            }

            SubjectKey = subjectKey;
        }
    }
}
