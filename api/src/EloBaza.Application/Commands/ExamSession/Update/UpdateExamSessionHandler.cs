using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.Subject;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.ExamSession.Update
{
    class UpdateExamSessionHandler : AsyncRequestHandler<UpdateExamSession>
    {
        private readonly IRepository<SubjectAggregate> _subjectRepository;

        public UpdateExamSessionHandler(IRepository<SubjectAggregate> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(UpdateExamSession request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with Key: {request.SubjectKey} does not exists");

            if (request.Data.Year.HasValue || !(request.Data.Semester is null))
                subject.UpdateExamSession(request.ExamSessionKey, request.Data.Year, request.Data.Semester);

            await _subjectRepository.Save(subject, cancellationToken);
        }
    }
}
