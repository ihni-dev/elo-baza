using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.SubjectAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.SubjectAggregate.Delete
{
    class DeleteSubjectHandler : AsyncRequestHandler<DeleteSubject>
    {
        private readonly IRepository<Subject> _subjectRepository;

        public DeleteSubjectHandler(IRepository<Subject> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(DeleteSubject request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with key: {request.SubjectKey} does not exists");

            subject.Delete(request.RequestorId);

            await _subjectRepository.Save(subject, cancellationToken);
        }
    }
}
