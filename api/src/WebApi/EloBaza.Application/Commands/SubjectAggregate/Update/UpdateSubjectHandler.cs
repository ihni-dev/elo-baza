using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.SubjectAggregate.Update
{
    class UpdateSubjectHandler : AsyncRequestHandler<UpdateSubject>
    {
        private readonly IRepository<Domain.SubjectAggregate.Subject> _subjectRepository;

        public UpdateSubjectHandler(IRepository<Domain.SubjectAggregate.Subject> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(UpdateSubject request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with key: {request.SubjectKey} does not exists");

            var nameChanged = !string.Equals(request.Data.Name, subject.Name, StringComparison.Ordinal);
            if (!string.IsNullOrWhiteSpace(request.Data.Name) && nameChanged)
                subject.UpdateName(request.RequestorId, request.Data.Name);

            await _subjectRepository.Save(subject, cancellationToken);
        }
    }
}
