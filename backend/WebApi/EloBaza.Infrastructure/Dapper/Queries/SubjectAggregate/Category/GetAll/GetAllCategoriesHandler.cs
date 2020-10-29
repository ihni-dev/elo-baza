//using Dapper;
//using EloBaza.Application.Queries.Common;
//using EloBaza.Application.Queries.SubjectAggregate.Category.GetAll;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace EloBaza.Infrastructure.Dapper.Queries.SubjectAggregate.Category.GetAll
//{
//    class GetAllCategoriesHandler : IRequestHandler<GetAllCategories, GetAllCategoriesResult>
//    {
//        private readonly IDbConnection _dbConnection;

//        private const string GetAllExamSessionsQuery = @"
//WITH CategoryResult AS (
//    SELECT
//        es.ExamSessionId,
//        es.ExamSessionKey,
//        es.Name
//    FROM Subject s
//    INNER JOIN ExamSession es ON s.SubjectId
//    WHERE 
//        s.SubjectKey = @SubjectKey
//        es.Name LIKE '%' + @Name + '%'
//), TotalCount AS (
//    SELECT COUNT(*) AS TotalCount 
//    FROM ExamSessionResult
//)
//SELECT
//    ExamSessionKey AS 'Key',
//    Name,
//    TotalCount
//FROM 
//    ExamSessionResult, 
//    TotalCount
//ORDER BY ExamSessionResult.ExamSessionId
//    OFFSET (@Page - 1) * @PageSize ROWS
//    FETCH NEXT @PageSize ROWS ONLY
//";

//        public GetAllCategoriesHandler(IDbConnection dbConnection)
//        {
//            _dbConnection = dbConnection;
//        }

//        public async Task<GetAllCategoriesResult> Handle(GetAllCategories request, CancellationToken cancellationToken)
//        {
//            var totalCountSet = new HashSet<int>();
//            Func<CategoryReadModel, int, CategoryReadModel> map = (result, totalCount) =>
//            {
//                totalCountSet.Add(totalCount);
//                return result;
//            };

//            var examSessions = await _dbConnection.QueryAsync(
//                sql: GetAllExamSessionsQuery,
//                map: map,
//                param: new
//                {
//                    request.SubjectKey,
//                    request.ExamSessionFilteringParameters.Name,
//                    request.PagingParameters.Page,
//                    request.PagingParameters.PageSize
//                },
//                splitOn: "TotalCount");

//            var pagingInfo = new PagingInfo(totalCountSet.SingleOrDefault(), request.PagingParameters.Page, request.PagingParameters.PageSize);
//            return new GetAllExamSessionsResult(examSessions, pagingInfo);
//        }
//    }
//}
