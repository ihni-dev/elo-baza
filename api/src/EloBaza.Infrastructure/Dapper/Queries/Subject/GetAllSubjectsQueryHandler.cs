using Dapper;
using EloBaza.Application.Queries.Common;
using EloBaza.Application.Queries.Subject;
using EloBaza.Application.Queries.Subject.GetAll;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.Dapper.Queries.Subject
{
    class GetAllSubjectsHandler : IRequestHandler<GetAllSubjects, GetAllSubjectsResult>
    {
        private readonly IDbConnection _dbConnection;

        private const string GetAllSubjectsQuery = @"
SELECT Name
FROM Subject
WHERE Name LIKE '%' + @Name + '%'
ORDER BY Id ASC
  OFFSET (@Page - 1) * @PageSize ROWS
  FETCH NEXT @PageSize ROWS ONLY;
";

        public GetAllSubjectsHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<GetAllSubjectsResult> Handle(GetAllSubjects request, CancellationToken cancellationToken)
        {
            var subjects = await _dbConnection.QueryAsync<SubjectReadModel>(
                GetAllSubjectsQuery, 
                new 
                {
                    request.SubjectFilteringParameters.Name,
                    request.PagingParameters.Page,
                    request.PagingParameters.PageSize
                });

            var pagingInfo = new PagingInfo(1, request.PagingParameters.Page, request.PagingParameters.PageSize);
            return new GetAllSubjectsResult(subjects, pagingInfo);
        }
    }
}
