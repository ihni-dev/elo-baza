using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.SubjectAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.SubjectAggregate.Category.Restore
{
    class RestoreCategoryHandler : AsyncRequestHandler<RestoreCategory>
    {
        private readonly IRepository<Subject> _subjectRepository;

        public RestoreCategoryHandler(IRepository<Subject> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(RestoreCategory request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with key: {request.SubjectKey} does not exists");

            subject.RestoreCategory(request.RequestorId, request.CategoryKey);

            await _subjectRepository.Save(subject, cancellationToken);
        }
    }
}
