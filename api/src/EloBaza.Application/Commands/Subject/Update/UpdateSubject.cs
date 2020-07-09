using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.Subject.Update
{
    public class UpdateSubject : IRequest
    {
        public Guid SubjectKey { get; private set; }
        public UpdateSubjectData Data { get; private set; }

        public UpdateSubject(Guid subjectKey, UpdateSubjectData data)
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
