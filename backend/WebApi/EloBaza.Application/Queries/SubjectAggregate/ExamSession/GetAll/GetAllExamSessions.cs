using EloBaza.Application.Queries.Common;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;

namespace EloBaza.Application.Queries.SubjectAggregate.ExamSession.GetAll
{
    public class GetAllExamSessions : PagedQuery, IRequest<GetAllExamSessionsResult>
    {
        public Guid SubjectKey { get; private set; }
        public ExamSessionFilteringParameters ExamSessionFilteringParameters { get; private set; }

        public GetAllExamSessions(Guid subjectKey, ExamSessionFilteringParameters examSessionFilteringParameters, PagingParameters pagingParameters)
            : base(pagingParameters)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => subjectKey == default, nameof(subjectKey), "Subject key must be provided");
            }

            ExamSessionFilteringParameters = examSessionFilteringParameters;
            SubjectKey = subjectKey;
        }
    }
}
