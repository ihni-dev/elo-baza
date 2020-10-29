using EloBaza.Application.Queries.SubjectAggregate.Get;
using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SubjectAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.SubjectAggregate.Create
{
    class CreateSubjectHandler : IRequestHandler<CreateSubject, SubjectDetailsReadModel>
    {
        private readonly IRepository<Subject> _subjectRepository;
        private readonly IMediator _mediator;

        public CreateSubjectHandler(IRepository<Subject> subjectRepository, IMediator mediator)
        {
            _subjectRepository = subjectRepository;
            _mediator = mediator;
        }

        public async Task<SubjectDetailsReadModel> Handle(CreateSubject request, CancellationToken cancellationToken)
        {
            var subject = Subject.Create(request.RequestorId, request.Data.Name);

            await _subjectRepository.Save(subject, cancellationToken);

            return await _mediator.Send(new GetSubjectDetails(subject.Key), cancellationToken);
        }
    }
}
