using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.Subject;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.Subject.Delete
{
    class DeleteSubjectHandler : AsyncRequestHandler<DeleteSubject>
    {
        private readonly IRepository<SubjectAggregate> _subjectRepository;

        public DeleteSubjectHandler(IRepository<SubjectAggregate> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(DeleteSubject request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with Key: {request.SubjectKey} does not exists");

            subject.Delete(userId: 1);

            await _subjectRepository.Save(subject, cancellationToken);
        }
    }
}
