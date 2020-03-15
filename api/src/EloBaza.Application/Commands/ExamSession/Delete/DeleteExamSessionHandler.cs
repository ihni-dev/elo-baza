using EloBaza.Application.Contracts;
using EloBaza.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.ExamSession.Delete
{
    class DeleteExamSessionHandler : AsyncRequestHandler<DeleteExamSession>
    {
        private readonly ISubjectRepository _subjectRepository;

        public DeleteExamSessionHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(DeleteExamSession request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectName, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with Name: {request.SubjectName} does not exists");

            subject.DeleteExamSession(request.Name);

            await _subjectRepository.SaveChanges(cancellationToken);
        }
    }
}
