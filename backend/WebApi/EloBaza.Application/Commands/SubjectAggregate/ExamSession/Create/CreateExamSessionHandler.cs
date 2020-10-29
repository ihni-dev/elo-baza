using EloBaza.Application.Queries.SubjectAggregate.ExamSession.Get;
using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.SubjectAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.SubjectAggregate.ExamSession.Create
{
    class CreateExamSessionHandler : IRequestHandler<CreateExamSession, ExamSessionDetailsReadModel>
    {
        private readonly IRepository<Subject> _subjectRepository;
        private readonly IMediator _mediator;

        public CreateExamSessionHandler(IRepository<Subject> subjectRepository, IMediator mediator)
        {
            _subjectRepository = subjectRepository;
            _mediator = mediator;
        }

        public async Task<ExamSessionDetailsReadModel> Handle(CreateExamSession request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with Key: {request.SubjectKey} does not exists");

            var examSession = subject.CreateExamSession(request.RequestorId, request.Data.Year, request.Data.Semester, request.Data.ResitNumber);

            await _subjectRepository.Save(subject, cancellationToken);

            return await _mediator.Send(new GetExamSessionDetails(subject.Key, examSession.Key), cancellationToken);
        }
    }
}
