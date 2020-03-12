using EloBaza.Application.Contracts;
using EloBaza.Application.Queries.Subject;
using EloBaza.Domain;
using EloBaza.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.Create
{
    class CreateSubjectHandler : IRequestHandler<CreateSubject, SubjectReadModel>
    {
        private readonly ISubjectRepository _subjectRepository;

        public CreateSubjectHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<SubjectReadModel> Handle(CreateSubject request, CancellationToken cancellationToken)
        {
            var subject = new Subject(request.Data.Name);

            if (await _subjectRepository.Exists(subject, cancellationToken))
                throw new AlreadyExistsException($"Subject {request.Data.Name} already exists");

            await _subjectRepository.Add(subject, cancellationToken);

            return new SubjectReadModel(subject.Id, subject.Name);
        }
    }
}
