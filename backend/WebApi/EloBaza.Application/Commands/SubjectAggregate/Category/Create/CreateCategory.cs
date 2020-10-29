using EloBaza.Application.Commands.Common;
using EloBaza.Application.Queries.SubjectAggregate.Category.Get;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Commands.SubjectAggregate.Category.Create
{
    public class CreateCategory : AuditableCommand, IRequest<CategoryDetailsReadModel>
    {
        public Guid SubjectKey { get; private set; }
        public CreateCategoryData Data { get; private set; }

        public CreateCategory(int requestorId, Guid subjectKey, CreateCategoryData data) : base(requestorId)
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
