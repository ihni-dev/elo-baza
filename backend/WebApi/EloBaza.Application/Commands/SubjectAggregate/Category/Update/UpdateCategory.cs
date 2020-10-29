using EloBaza.Application.Commands.Common;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.SubjectAggregate.Category.Update
{
    public class UpdateCategory : AuditableCommand, IRequest
    {
        public Guid SubjectKey { get; private set; }
        public Guid CategoryKey { get; private set; }
        public UpdateCategoryData Data { get; private set; }

        public UpdateCategory(int requestorId, Guid subjectKey, Guid categoryKey, UpdateCategoryData data) : base(requestorId)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject key must be provided");
                validationContext.Validate(() => categoryKey == default, nameof(categoryKey), "Category key must be provided");
            }

            SubjectKey = subjectKey;
            CategoryKey = categoryKey;
            Data = data;
        }
    }
}
