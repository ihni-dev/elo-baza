using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.Subject;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.Subject.Update
{
    class UpdateSubjectHandler : AsyncRequestHandler<UpdateSubject>
    {
        private readonly IRepository<SubjectAggregate> _subjectRepository;

        public UpdateSubjectHandler(IRepository<SubjectAggregate> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(UpdateSubject request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with Key: {request.SubjectKey} does not exists");

            var nameChanged = !string.Equals(request.Data.Name, subject.Name, StringComparison.OrdinalIgnoreCase);
            if (!string.IsNullOrWhiteSpace(request.Data.Name) && nameChanged)
            {
                subject.UpdateName(request.Data.Name);
            }

            await _subjectRepository.Save(subject, cancellationToken);
        }
    }
}
