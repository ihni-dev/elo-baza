using EloBaza.Application.Queries.Subject.Get;
using EloBaza.Application.Queries.Subject.GetAll;
using EloBaza.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.Subject.Create
{
    class CreateSubjectHandler : IRequestHandler<CreateSubject, SubjectReadModel>
    {
        private readonly IRepository<Domain.SubjectAggregate.Subject> _subjectRepository;
        private readonly IMediator _mediator;

        public CreateSubjectHandler(IRepository<Domain.SubjectAggregate.Subject> subjectRepository, IMediator mediator)
        {
            _subjectRepository = subjectRepository;
            _mediator = mediator;
        }

        public async Task<SubjectReadModel> Handle(CreateSubject request, CancellationToken cancellationToken)
        {
            var subject = new Domain.SubjectAggregate.Subject(request.Data.Name, request.RequestorId);

            await _subjectRepository.Save(subject, cancellationToken);

            return await _mediator.Send(new GetSubjectDetails(subject.Key));
        }
    }
}
