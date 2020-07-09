using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.Subject.Delete
{
    public class DeleteSubject : IRequest
    {
        public Guid SubjectKey { get; private set; }

        public DeleteSubject(Guid subjectKey)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject Key must be provided");
            }

            SubjectKey = subjectKey;
        }
    }
}
