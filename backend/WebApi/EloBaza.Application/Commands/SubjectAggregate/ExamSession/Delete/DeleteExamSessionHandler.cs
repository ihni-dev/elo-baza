﻿using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using EloBaza.Domain.SubjectAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.SubjectAggregate.ExamSession.Delete
{
    class DeleteExamSessionHandler : AsyncRequestHandler<DeleteExamSession>
    {
        private readonly IRepository<Subject> _subjectRepository;

        public DeleteExamSessionHandler(IRepository<Subject> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        protected override async Task Handle(DeleteExamSession request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.SubjectKey, cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Subject with key: {request.SubjectKey} does not exists");

            subject.DeleteExamSession(request.RequestorId, request.ExamSessionKey);

            await _subjectRepository.Save(subject, cancellationToken);
        }
    }
}
