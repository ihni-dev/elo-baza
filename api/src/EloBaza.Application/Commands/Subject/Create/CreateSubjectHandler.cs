using EloBaza.Application.Queries.Subject.GetAll;
using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.Subject;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.Subject.Create
{
    class CreateSubjectHandler : IRequestHandler<CreateSubject, SubjectReadModel>
    {
        private readonly IRepository<SubjectAggregate> _subjectRepository;

        public CreateSubjectHandler(IRepository<SubjectAggregate> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<SubjectReadModel> Handle(CreateSubject request, CancellationToken cancellationToken)
        {
            var subject = new SubjectAggregate(request.Data.Name);

            await _subjectRepository.Save(subject, cancellationToken);

            return new SubjectReadModel()
            {
                Name = subject.Name
            };
        }
    }
}
