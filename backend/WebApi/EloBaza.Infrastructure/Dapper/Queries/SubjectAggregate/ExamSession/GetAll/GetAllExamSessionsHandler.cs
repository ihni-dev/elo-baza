using Dapper;
using EloBaza.Application.Queries.SubjectAggregate.ExamSession.GetAll;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.Dapper.Queries.SubjectAggregate.ExamSession.GetAll
{
    class GetAllExamSessionsHandler : IRequestHandler<GetAllExamSessions, GetAllExamSessionsResult>
    {
        private readonly IDbConnection _dbConnection;

        private const string GetAllExamSessionsQuery = @"
SELECT
    es.ExamSessionId,
    es.ExamSessionKey,
    es.Name
FROM Subject s
INNER JOIN ExamSession es ON s.SubjectId = es.SubjectId
";

        public GetAllExamSessionsHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<GetAllExamSessionsResult> Handle(GetAllExamSessions request, CancellationToken cancellationToken)
        {
            var examSessions = await _dbConnection.QueryAsync<ExamSessionReadModel>(
                sql: GetAllExamSessionsQuery,
                param: new
                {
                    request.SubjectKey,
                });

            return new GetAllExamSessionsResult(examSessions);
        }
    }
}
