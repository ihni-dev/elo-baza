using EloBaza.Application.Queries.ExamSession;
using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.Subject;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.ExamSession.Create
{
    class CreateExamSessionHandler : IRequestHandler<CreateExamSession, ExamSessionReadModel>
    {
        private readonly IRepository<SubjectAggregate> _subjectRepository;

        public CreateExamSessionHandler(IRepository<SubjectAggregate> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<ExamSessionReadModel> Handle(CreateExamSession request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with Key: {request.SubjectKey} does not exists");

            var examSessionName = subject.CreateExamSession(request.Data.Year, request.Data.Semester);

            await _subjectRepository.Save(subject, cancellationToken);

            return new ExamSessionReadModel()
            {
                Name = examSessionName.Name,
                Year = examSessionName.Year,
                Semester = examSessionName.Semester
            };
        }
    }
}
