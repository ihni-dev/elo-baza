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
            var result = await _subjectRepository.GetAll(
                s => s.Name.Contains(request.SubjectFilteringParameters.Name), 
                request.PagingParameters,
                cancellationToken);

            var subjectsReadModels = result.Data.Select(s => new SubjectReadModel(s.Id, s.Name));
            var pagingInfo = new PagingInfo(result.TotalCount, request.PagingParameters.Page, request.PagingParameters.PageSize);

            return new GetAllSubjectsResult(subjectsReadModels, pagingInfo);
        }
    }
}
