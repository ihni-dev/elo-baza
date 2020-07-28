using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.SubjectAggregate.ExamSession.Update
{
    class UpdateExamSessionHandler : AsyncRequestHandler<UpdateExamSession>
    {
        private readonly IRepository<Domain.SubjectAggregate.Subject> _subjectRepository;

        public UpdateExamSessionHandler(IRepository<Domain.SubjectAggregate.Subject> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(UpdateExamSession request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with key: {request.SubjectKey} does not exists");

            if (request.Data.Year.HasValue || !(request.Data.Semester is null))
                subject.UpdateExamSession(request.RequestorId, request.ExamSessionKey, request.Data.Year, request.Data.Semester, request.Data.ResitNumber);

            await _subjectRepository.Save(subject, cancellationToken);
        }
    }
}
