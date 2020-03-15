using EloBaza.Domain.SharedKernel;
using MediatR;

namespace EloBaza.Application.Commands.ExamSession.Update
{
    public class UpdateExamSession : IRequest
    {
        public string SubjectName { get; private set; }
        public string Name { get; private set; }
        public UpdateExamSessionData Data { get; private set; }

        public UpdateExamSession(string subjectName, string name, UpdateExamSessionData data)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => string.IsNullOrWhiteSpace(subjectName), nameof(subjectName), "Subject name must be provided");
                validationContext.Validate(() => string.IsNullOrWhiteSpace(name), nameof(name), "Exam session name must be provided");
            }

            SubjectName = subjectName;
            Name = name;
            Data = data;
        }
    }
}
