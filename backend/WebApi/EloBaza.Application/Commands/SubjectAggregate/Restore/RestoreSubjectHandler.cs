using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.SubjectAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.SubjectAggregate.Restore
{
    class RestoreSubjectHandler : AsyncRequestHandler<RestoreSubject>
    {
        private readonly IRepository<Subject> _subjectRepository;

        public RestoreSubjectHandler(IRepository<Subject> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(RestoreSubject request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with key: {request.SubjectKey} does not exists");

            subject.Restore(request.RequestorId);

            await _subjectRepository.Save(subject, cancellationToken);
        }
    }
}
