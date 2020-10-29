using EloBaza.Application.Queries.SubjectAggregate.Category.Get;
using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.SubjectAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.SubjectAggregate.Category.Create
{
    class CreateCategoryHandler : IRequestHandler<CreateCategory, CategoryDetailsReadModel>
    {
        private readonly IRepository<Subject> _subjectRepository;
        private readonly IMediator _mediator;

        public CreateCategoryHandler(IRepository<Subject> subjectRepository, IMediator mediator)
        {
            _subjectRepository = subjectRepository;
            _mediator = mediator;
        }

        public async Task<CategoryDetailsReadModel> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with Key: {request.SubjectKey} does not exists");

            var category = subject.CreateCategory(request.RequestorId, request.Data.Name, request.Data.ParentCateogryKey);

            await _subjectRepository.Save(subject, cancellationToken);

            return await _mediator.Send(new GetCategoryDetails(subject.Key, category.Key), cancellationToken);
        }
    }
}
