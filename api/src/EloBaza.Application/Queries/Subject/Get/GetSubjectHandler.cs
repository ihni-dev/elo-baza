using EloBaza.Application.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Queries.Subject.Get
{
    class GetSubjectHandler : IRequestHandler<GetSubject, SubjectReadModel>
    {
        private readonly ISubjectRepository _subjectRepository;

        public GetSubjectHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }


        public async Task<SubjectReadModel> Handle(GetSubject request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.Find(request.Name, cancellationToken);

            return new SubjectReadModel(subject.Name);
        }
    }
}
