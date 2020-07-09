using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.Subject;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.ExamSession.Delete
{
    class DeleteExamSessionHandler : AsyncRequestHandler<DeleteExamSession>
    {
        private readonly IRepository<SubjectAggregate> _subjectRepository;

        public DeleteExamSessionHandler(IRepository<SubjectAggregate> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(DeleteExamSession request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with Key: {request.SubjectKey} does not exists");

            subject.DeleteExamSession(request.ExamSessionKey);

            await _subjectRepository.Save(subject, cancellationToken);
        }
    }
}
