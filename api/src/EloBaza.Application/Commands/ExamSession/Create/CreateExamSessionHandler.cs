using EloBaza.Application.Contracts;
using EloBaza.Application.Queries.ExamSession;
using EloBaza.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.ExamSession.Create
{
    class CreateExamSessionHandler : IRequestHandler<CreateExamSession, ExamSessionReadModel>
    {
        private readonly ISubjectRepository _subjectRepository;

        public CreateExamSessionHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<ExamSessionReadModel> Handle(CreateExamSession request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectName);
            if (subject is null)
                throw new NotFoundException($"Subject with Name: {request.SubjectName} does not exists");

            var examSessionName = subject.CreateExamSession(request.Data.Year, request.Data.Semester);

            await _subjectRepository.SaveChanges(cancellationToken);

            return new ExamSessionReadModel() 
            {
                Name = examSessionName.Name,
                Year = examSessionName.Year, 
                Semester = examSessionName.Semester 
            };
        }
    }
}
