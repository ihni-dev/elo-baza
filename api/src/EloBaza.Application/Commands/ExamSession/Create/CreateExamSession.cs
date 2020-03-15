using EloBaza.Application.Queries.ExamSession;
using EloBaza.Domain.SharedKernel;
using MediatR;

namespace EloBaza.Application.Commands.ExamSession.Create
{
    public class CreateExamSession : IRequest<ExamSessionReadModel>
    {
        public string SubjectName { get; private set; }
        public CreateExamSessionData Data { get; private set; }

        public CreateExamSession(string subjectName, CreateExamSessionData data)
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
