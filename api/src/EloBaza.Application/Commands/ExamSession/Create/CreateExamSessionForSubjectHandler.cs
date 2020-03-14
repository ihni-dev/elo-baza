using EloBaza.Application.Contracts;
using EloBaza.Application.Queries.ExamSession;
using EloBaza.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.ExamSession.Create
{
    class CreateExamSessionForSubjectHandler : IRequestHandler<CreateExamSessionForSubject, ExamSessionReadModel>
    {
        private readonly ISubjectRepository _subjectRepository;

        public CreateExamSessionForSubjectHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<ExamSessionReadModel> Handle(CreateExamSessionForSubject request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectName);
            if (subject is null)
            {
                throw new NotFoundException($"Subject with Name: {request.SubjectName} does not exists");
            }

            subject.CreateExamSession(request.Data.Year, request.Data.Semester);

            await _subjectRepository.SaveChanges(cancellationToken);

            return new ExamSessionReadModel() 
            {
                SubjectName = subject.Name, 
                Year = request.Data.Year, 
                Semester = request.Data.Semester 
            };
        }
    }
}
