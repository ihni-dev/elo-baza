using Dapper;
using EloBaza.Application.Queries.ExamSession.Get;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.Dapper.Queries.ExamSession.Get
{
    class GetExamSessionDetailsHandler : IRequestHandler<GetExamSessionDetails, ExamSessionDetailsReadModel>
    {
        private readonly IDbConnection _dbConnection;

        private const string GetExamSessionDetailsQuery = @"
SELECT es.Name,
    es.Year,
    es.Semester
FROM Subject s
    INNER JOIN ExamSession es ON s.Id = es.SubjectId 
WHERE s.Name = @SubjectName AND es.Name = @Name
";

        public GetExamSessionDetailsHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ExamSessionDetailsReadModel> Handle(GetExamSessionDetails request, CancellationToken cancellationToken)
        {
            var examSession = await _dbConnection.QuerySingleOrDefaultAsync<ExamSessionDetailsReadModel>(
                sql: GetExamSessionDetailsQuery,
                param: new { request.SubjectName, request.Name });

            if (examSession is null)
                throw new NotFoundException($"Exam session {request.Name} not found for subject {request.SubjectName}");

            return examSession;
        }
    }
}