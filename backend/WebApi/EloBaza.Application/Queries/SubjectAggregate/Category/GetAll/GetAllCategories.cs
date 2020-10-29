using EloBaza.Application.Queries.Common;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Queries.SubjectAggregate.Category.GetAll
{
    public class GetAllCategories : PagedQuery, IRequest<GetAllCategoriesResult>
    {
        public Guid SubjectKey { get; private set; }
        public CategoryFilteringParameters CategoryFilteringParameters { get; private set; }

        public GetAllCategories(Guid subjectKey, CategoryFilteringParameters categoryFilteringParameters, PagingParameters pagingParameters)
            : base(pagingParameters)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject key must be provided");
            }

            CategoryFilteringParameters = categoryFilteringParameters;
            SubjectKey = subjectKey;
        }
    }
}
