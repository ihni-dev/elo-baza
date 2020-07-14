using Dapper;
using EloBaza.Application.Queries.ExamSession;
using EloBaza.Application.Queries.Subject.Get;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.Dapper.Queries.Subject.Get
{
    class GetSubjectDetailsHandler : IRequestHandler<GetSubjectDetails, SubjectDetailsReadModel>
    {
        private readonly IDbConnection _dbConnection;

        private const string GetSubjectQuery = @"
SELECT 
    s.SubjectKey AS 'Key',
    s.Name,
    es.ExamSessionKey AS 'Key',
    es.Name,
    es.Year,
    es.Semester
FROM Subject s
    LEFT JOIN ExamSession es ON s.SubjectId = es.SubjectId 
WHERE s.SubjectKey = @SubjectKey
";

        public GetSubjectDetailsHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<SubjectDetailsReadModel> Handle(GetSubjectDetails request, CancellationToken cancellationToken)
        {
            var lookup = new Dictionary<Guid, SubjectDetailsReadModel>();

            await _dbConnection.QueryAsync<SubjectDetailsReadModel, ExamSessionReadModel, SubjectDetailsReadModel>(
                sql: GetSubjectQuery,
                map: (sdrm, esrm) =>
                {
                    SubjectDetailsReadModel subjectDetailsReadModel;

                    if (!lookup.TryGetValue(sdrm.Key, out subjectDetailsReadModel))
                        lookup.Add(sdrm.Key, subjectDetailsReadModel = sdrm);

                    if (!(esrm is null))
                        subjectDetailsReadModel.ExamSessions.Add(esrm);

                    return subjectDetailsReadModel;
                },
                param: new { request.SubjectKey },
                splitOn: "Key");

            var subject = lookup.GetValueOrDefault(request.SubjectKey);
            if (subject is null)
                throw new NotFoundException($"Subject {request.SubjectKey} not found");

            return subject;
        }
    }
}
