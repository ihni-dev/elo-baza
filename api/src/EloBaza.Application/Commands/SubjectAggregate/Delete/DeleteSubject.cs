using EloBaza.Application.Commands.Common;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.SubjectAggregate.Delete
{
    public class DeleteSubject : AuditableCommand, IRequest
    {
        public Guid SubjectKey { get; private set; }

        public DeleteSubject(int requestorId, Guid subjectKey) : base(requestorId)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject's key must be provided");
            }

            SubjectKey = subjectKey;
        }
    }
}
