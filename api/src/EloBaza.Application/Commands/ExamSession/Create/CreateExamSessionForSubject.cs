using EloBaza.Application.Queries.ExamSession;
using EloBaza.Domain.SharedKernel;
using MediatR;

namespace EloBaza.Application.Commands.ExamSession.Create
{
    public class CreateExamSessionForSubject : IRequest<ExamSessionReadModel>
    {
        public string SubjectName { get; private set; }
        public CreateExamSessionForSubjectData Data { get; private set; }

        public CreateExamSessionForSubject(string subjectName, CreateExamSessionForSubjectData data)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(subjectName), nameof(subjectName), "Subject name must be provided");
            }

            SubjectName = subjectName;
            Data = data;
        }
    }
}
