using EloBaza.Application.Contracts;
using EloBaza.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.Delete
{
    class DeleteSubjectHandler : AsyncRequestHandler<DeleteSubject>
    {
        private readonly ISubjectRepository _subjectRepository;

        public DeleteSubjectHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(DeleteSubject request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.Name, cancellationToken);
            if (subject is null)
            {
                throw new NotFoundException($"Subject with Id: {request.Name} does not exists");
            }

            _subjectRepository.Delete(subject);
            await _subjectRepository.SaveChanges(cancellationToken);
        }
    }
}
