using Dapper;
using EloBaza.Application.Queries.Common;
using EloBaza.Application.Queries.Subject.GetAll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.Dapper.Queries.Subject.Get
{
    class GetAllSubjectsHandler : IRequestHandler<GetAllSubjects, GetAllSubjectsResult>
    {
        private readonly IDbConnection _dbConnection;

        private const string GetAllSubjectsQuery = @"
WITH SubjectResult AS (
    SELECT
        SubjectId,
        SubjectKey,
        Name
    FROM Subject
    WHERE Name LIKE '%' + @Name + '%'
), TotalCount AS (
    SELECT COUNT(*) AS TotalCount 
    FROM SubjectResult
)
SELECT
    SubjectKey AS 'Key',
    Name,
    TotalCount
FROM 
    SubjectResult, 
    TotalCount
ORDER BY SubjectResult.SubjectId
    OFFSET (@Page - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY
";

        public GetAllSubjectsHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<GetAllSubjectsResult> Handle(GetAllSubjects request, CancellationToken cancellationToken)
        {
            var totalCountSet = new HashSet<int>();
            Func<SubjectReadModel, int, SubjectReadModel> map = (result, totalCount) =>
            {
                totalCountSet.Add(totalCount);
                return result;
            };

            var subjects = await _dbConnection.QueryAsync(
                sql: GetAllSubjectsQuery,
                map: map,
                param: new
                {
                    request.SubjectFilteringParameters.Name,
                    request.PagingParameters.Page,
                    request.PagingParameters.PageSize
                },
                splitOn: "TotalCount");

            var pagingInfo = new PagingInfo(totalCountSet.SingleOrDefault(), request.PagingParameters.Page, request.PagingParameters.PageSize);
            return new GetAllSubjectsResult(subjects, pagingInfo);
        }
    }
}
