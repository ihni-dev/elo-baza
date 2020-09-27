using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.SubjectAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.SubjectAggregate.Update
{
    class UpdateSubjectHandler : AsyncRequestHandler<UpdateSubject>
    {
        private readonly IRepository<Subject> _subjectRepository;

        public UpdateSubjectHandler(IRepository<Subject> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(UpdateSubject request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with key: {request.SubjectKey} does not exists");

            subject.Update(request.RequestorId, request.Data.Name);

            await _subjectRepository.Save(subject, cancellationToken);
        }
    }
}
