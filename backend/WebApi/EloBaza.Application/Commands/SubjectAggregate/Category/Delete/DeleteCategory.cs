using EloBaza.Application.Commands.Common;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.SubjectAggregate.Category.Delete
{
    public class DeleteCategory : AuditableCommand, IRequest
    {
        public Guid SubjectKey { get; private set; }
        public Guid CategoryKey { get; private set; }

        public DeleteCategory(int requestorId, Guid subjectKey, Guid categoryKey) : base(requestorId)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject key must be provided");
                validationContext.Validate(() => CategoryKey == default, nameof(CategoryKey), "Category key must be provided");
            }

            SubjectKey = subjectKey;
            CategoryKey = categoryKey;
        }
    }
}
