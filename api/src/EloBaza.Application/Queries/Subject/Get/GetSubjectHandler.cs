using EloBaza.Application.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Queries.Subject.Get
{
    class GetSubjectHandler : IRequestHandler<GetSubject, GetSubjectResult>
    {
        private readonly ISubjectRepository _subjectRepository;

        public GetSubjectHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }


        public async Task<GetSubjectResult> Handle(GetSubject request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.Id);

            return new GetSubjectResult(new SubjectReadModel(subject.Name));
        }
    }
}
