using EloBaza.Application.Contracts;
using EloBaza.Domain.SharedKernel;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.Update
{
    class UpdateSubjectHandler : IRequestHandler<UpdateSubject>
    {
        private readonly ISubjectRepository _subjectRepository;

        public UpdateSubjectHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Unit> Handle(UpdateSubject request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.Id, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with Id: {request.Id} does not exists");

            if (!(request.Data.Name is null))
            {
                var nameChanged = !string.Equals(request.Data.Name, subject.Name, StringComparison.OrdinalIgnoreCase);
                if (await _subjectRepository.Exists(request.Data.Name, cancellationToken) && nameChanged)
                    throw new AlreadyExistsException($"Subject with name: {request.Data.Name} already exists");

                subject.UpdateName(request.Data.Name);
            }

            await _subjectRepository.Update(subject, cancellationToken);

            return Unit.Value;
        }
    }
}
