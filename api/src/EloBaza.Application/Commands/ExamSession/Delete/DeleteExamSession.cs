using EloBaza.Domain.SharedKernel;
using MediatR;

namespace EloBaza.Application.Commands.ExamSession.Delete
{
    public class DeleteExamSession : IRequest
    {
        public string SubjectName { get; private set; }
        public string Name { get; private set; }

        public DeleteExamSession(string subjectName, string name)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(subjectName), nameof(subjectName), "Subject name must be provided");
                validationContext.Validate(() => string.IsNullOrWhiteSpace(name), nameof(name), "Exam session name must be provided");
            }

            SubjectName = subjectName;
            Name = name;
        }
    }
}
