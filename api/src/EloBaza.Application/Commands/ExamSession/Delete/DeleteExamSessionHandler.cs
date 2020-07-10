using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.ExamSession.Delete
{
    class DeleteExamSessionHandler : AsyncRequestHandler<DeleteExamSession>
    {
        private readonly IRepository<Domain.SubjectAggregate.Subject> _subjectRepository;

        public DeleteExamSessionHandler(IRepository<Domain.SubjectAggregate.Subject> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(DeleteExamSession request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with key: {request.SubjectKey} does not exists");

            subject.DeleteExamSession(request.ExamSessionKey, request.RequestorId);

            await _subjectRepository.Save(subject, cancellationToken);
        }
    }
}
