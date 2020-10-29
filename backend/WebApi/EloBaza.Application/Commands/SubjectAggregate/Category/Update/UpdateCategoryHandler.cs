using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.SubjectAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.SubjectAggregate.Category.Update
{
    class UpdateCategoryHandler : AsyncRequestHandler<UpdateCategory>
    {
        private readonly IRepository<Subject> _subjectRepository;

        public UpdateCategoryHandler(IRepository<Subject> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(UpdateCategory request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with key: {request.SubjectKey} does not exists");

            subject.UpdateCategory(request.RequestorId, request.CategoryKey, request.Data.Name, request.Data.ParentCateogryKey);

            await _subjectRepository.Save(subject, cancellationToken);
        }
    }
}
