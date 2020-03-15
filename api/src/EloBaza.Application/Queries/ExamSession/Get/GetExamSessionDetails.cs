using EloBaza.Domain.SharedKernel;
using MediatR;

namespace EloBaza.Application.Queries.ExamSession.Get
{
    public class GetExamSessionDetails : IRequest<ExamSessionDetailsReadModel>
    {
        public string SubjectName { get; private set; }
        public string Name { get; private set; }

        public GetExamSessionDetails(string subjectName, string name)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(subjectName), nameof(subjectName), "Not empty subject name must be provided");
                validationContext.Validate(() => string.IsNullOrWhiteSpace(name), nameof(name), "Not empty name must be provided");
            }

            SubjectName = subjectName;
            Name = name;
        }
    }
}
