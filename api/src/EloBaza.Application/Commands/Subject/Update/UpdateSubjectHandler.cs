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
            {
                throw new NotFoundException($"Subject with name: {request.Name} does not exists");
            }

            if (!(request.Data.Name is null))
            {
                var nameChanged = !string.Equals(request.Data.Name, subject.Name, StringComparison.OrdinalIgnoreCase);
                if (await _subjectRepository.Exists(request.Data.Name, cancellationToken) && nameChanged)
                {
                    throw new AlreadyExistsException($"Subject with name: {request.Data.Name} already exists");
                }

                subject.UpdateName(request.Data.Name);
            }

            _subjectRepository.Update(subject);
            await _subjectRepository.SaveChanges(cancellationToken);
        }
    }
}
