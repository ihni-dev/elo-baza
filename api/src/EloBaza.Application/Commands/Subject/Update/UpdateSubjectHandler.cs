using EloBaza.Application.Contracts;
using EloBaza.Domain.SharedKernel;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.Subject.Update
{
    class UpdateSubjectHandler : AsyncRequestHandler<UpdateSubject>
    {
        private readonly ISubjectRepository _subjectRepository;

        public UpdateSubjectHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(UpdateSubject request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.Name, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with name: {request.Name} does not exists");

            var nameChanged = !string.Equals(request.Data.Name, subject.Name, StringComparison.OrdinalIgnoreCase);
            if (!string.IsNullOrWhiteSpace(request.Data.Name) && nameChanged)
            {
                if (await _subjectRepository.Exists(request.Data.Name, cancellationToken))
                    throw new AlreadyExistsException($"Subject with name: {request.Data.Name} already exists");

                subject.UpdateName(request.Data.Name);
            }

            await _subjectRepository.SaveChanges(cancellationToken);
        }
    }
}
