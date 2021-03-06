﻿using Dapper;
using EloBaza.Application.Queries.SubjectAggregate.ExamSession.Get;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.Dapper.Queries.SubjectAggregate.ExamSession.Get
{
    class GetExamSessionDetailsHandler : IRequestHandler<GetExamSessionDetails, ExamSessionDetailsReadModel>
    {
        private readonly IDbConnection _dbConnection;

        private const string GetExamSessionDetailsQuery = @"
SELECT 
    es.ExamSessionKey AS 'Key',
    es.Name,
    es.Year,
    es.Semester,
    es.ResitNumber
FROM Subject s
INNER JOIN ExamSession es ON s.SubjectId = es.SubjectId 
WHERE s.SubjectKey = @SubjectKey AND es.ExamSessionKey = @ExamSessionKey
";

        public GetExamSessionDetailsHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ExamSessionDetailsReadModel> Handle(GetExamSessionDetails request, CancellationToken cancellationToken)
        {
            var examSession = await _dbConnection.QuerySingleOrDefaultAsync<ExamSessionDetailsReadModel>(
                sql: GetExamSessionDetailsQuery,
                param: new { request.SubjectKey, request.ExamSessionKey });

            if (examSession is null)
                throw new NotFoundException($"Exam session {request.ExamSessionKey} not found for subject {request.SubjectKey}");

            return examSession;
        }
    }
}