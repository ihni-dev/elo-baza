using MediatR;
using System;

namespace EloBaza.Application.Queries.SubjectAggregate.Category.Get
{
    public class GetCategoryDetails : IRequest<CategoryDetailsReadModel>
    {
        public Guid SubjectKey { get; private set; }
        public Guid CategoryKey { get; private set; }

        public GetCategoryDetails(Guid subjectKey, Guid categoryKey)
        {
            SubjectKey = subjectKey;
            CategoryKey = categoryKey;
        }
    }
}
