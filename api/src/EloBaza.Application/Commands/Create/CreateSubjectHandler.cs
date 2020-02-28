using EloBaza.Application.Contracts;
using EloBaza.Domain;
using EloBaza.Domain.SharedKernel;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.Create
{
    class CreateSubjectHandler : IRequestHandler<CreateSubject, Guid>
    {
        private readonly ISubjectRepository _subjectRepository;

        public CreateSubjectHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Guid> Handle(CreateSubject request, CancellationToken cancellationToken)
        {
            var subject = new Subject(request.Model.Name);

            if (await _subjectRepository.Exists(subject))
                throw new AlreadyExistsException($"Subject {request.Model.Name} already exists");

            await _subjectRepository.Save(subject);

            return subject.Id;
        }
    }
}
