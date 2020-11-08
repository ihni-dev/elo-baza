using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Queries.SubjectAggregate.Category.GetAll
{
    public class GetAllCategories : IRequest<GetAllCategoriesResult>
    {
        public Guid SubjectKey { get; private set; }

        public GetAllCategories(Guid subjectKey)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject key must be provided");
            }

            SubjectKey = subjectKey;
        }
    }
}
