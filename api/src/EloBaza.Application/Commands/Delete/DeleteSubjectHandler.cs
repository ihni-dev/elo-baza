using EloBaza.Application.Contracts;
using EloBaza.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.Delete
{
    class DeleteSubjectHandler : IRequestHandler<DeleteSubject>
    {
        private readonly ISubjectRepository _subjectRepository;

        public DeleteSubjectHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Unit> Handle(DeleteSubject request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.Id, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with Id: {request.Id} does not exists");

            await _subjectRepository.Delete(subject, cancellationToken);

            return Unit.Value;
        }
    }
}
