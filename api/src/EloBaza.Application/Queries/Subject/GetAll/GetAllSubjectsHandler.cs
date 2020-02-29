using EloBaza.Application.Contracts;
using EloBaza.Application.Queries.Common;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Queries.Subject.GetAll
{
    class GetAllSubjectsHandler : IRequestHandler<GetAllSubjects, GetAllSubjectsResult>
    {
        private readonly ISubjectRepository _subjectRepository;

        public GetAllSubjectsHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<GetAllSubjectsResult> Handle(GetAllSubjects request, CancellationToken cancellationToken)
        {
            var subjects = await _subjectRepository.GetAll(
                s => s.Name.Contains(request.SubjectFilteringParameters.Name), 
                request.PagingParameters,
                cancellationToken);

            var totalCount = await _subjectRepository.GetTotalCount(s => s.Name.Contains(request.SubjectFilteringParameters.Name), cancellationToken);

            var subjectsReadModels = subjects.Select(s => new SubjectReadModel(s.Id, s.Name));
            var pagingInfo = new PagingInfo(totalCount, request.PagingParameters.PageIndex, request.PagingParameters.PageSize);

            return new GetAllSubjectsResult(subjectsReadModels, pagingInfo);
        }
    }
}
