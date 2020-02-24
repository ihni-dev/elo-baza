using EloBaza.Application.Contracts;
using EloBaza.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Commands.Create
{
    class CreateSubjectHandler : IRequestHandler<CreateSubject, Guid>
    {
        private readonly ISubjectRepository _subjectRepository;

        public CreateSubjectHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Guid> Handle(CreateSubject request, CancellationToken cancellationToken)
        {
            var subject = new Subject(request.Model.Name);

            try
            {
                await _subjectRepository.Save(subject);
            }
            catch (Exception)
            {

                throw;
            }

            return subject.Id;
        }
    }
}
