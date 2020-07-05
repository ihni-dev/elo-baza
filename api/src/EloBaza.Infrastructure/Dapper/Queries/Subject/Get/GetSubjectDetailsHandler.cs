using Dapper;
using EloBaza.Application.Queries.ExamSession;
using EloBaza.Application.Queries.Subject.Get;
using EloBaza.Domain.SharedKernel;
using MediatR;
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
SELECT s.Name,
    es.Name,
    es.Year,
    es.Semester
FROM Subject s
    INNER JOIN ExamSession es ON s.Id = es.SubjectId 
WHERE s.Name = @Name
";

        public GetSubjectDetailsHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<SubjectDetailsReadModel> Handle(GetSubjectDetails request, CancellationToken cancellationToken)
        {
            var lookup = new Dictionary<string, SubjectDetailsReadModel>();

            await _dbConnection.QueryAsync<SubjectDetailsReadModel, ExamSessionReadModel, SubjectDetailsReadModel>(
                sql: GetSubjectQuery,
                map: (sdrm, esrm) =>
                {
                    SubjectDetailsReadModel subjectDetailsReadModel;

                    if (!lookup.TryGetValue(sdrm.Name!, out subjectDetailsReadModel))
                        lookup.Add(sdrm.Name!, subjectDetailsReadModel = sdrm);

                    subjectDetailsReadModel.ExamSessions.Add(esrm);

                    return subjectDetailsReadModel;
                },
                param: new { request.Name },
                splitOn: "Name");

            var subject = lookup.GetValueOrDefault(request.Name);
            if (subject is null)
                throw new NotFoundException($"Subject {request.Name} not found");

            return subject;
        }
    }
}
