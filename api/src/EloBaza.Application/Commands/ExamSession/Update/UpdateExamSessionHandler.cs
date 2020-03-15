using EloBaza.Application.Contracts;
using EloBaza.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.ExamSession.Update
{
    class UpdateExamSessionHandler : AsyncRequestHandler<UpdateExamSession>
    {
        private readonly ISubjectRepository _subjectRepository;

        public UpdateExamSessionHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(UpdateExamSession request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectName, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with name: {request.SubjectName} does not exists");

            if (request.Data.Year.HasValue || request.Data.Semester.HasValue)
                subject.UpdateExamSession(request.Name, request.Data.Year, request.Data.Semester);

            await _subjectRepository.SaveChanges(cancellationToken);
        }
    }
}
