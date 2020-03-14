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
    class GetSubjectHandler : IRequestHandler<GetSubject, SubjectDetailsReadModel>
    {
        private readonly IDbConnection _dbConnection;

        private const string GetSubjectQuery = @"
SELECT s.Name,
    es.SubjectName,
    es.Year,
    es.Semester
FROM Subject s
    INNER JOIN ExamSession es ON s.Id = es.SubjectId 
WHERE Name = @Name
";

        public GetSubjectHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<SubjectDetailsReadModel> Handle(GetSubject request, CancellationToken cancellationToken)
        {
            var lookup = new Dictionary<string, SubjectDetailsReadModel>();

            await _dbConnection.QueryAsync<SubjectDetailsReadModel, ExamSessionReadModel, SubjectDetailsReadModel>(
                GetSubjectQuery,
                map: (sdrm, esrm) =>
                {
                    SubjectDetailsReadModel subjectDetailsReadModel;

                    if (!lookup.TryGetValue(sdrm.Name, out subjectDetailsReadModel))
                        lookup.Add(sdrm.Name, subjectDetailsReadModel = sdrm);

                    if (subjectDetailsReadModel.ExamSessions == null)
                        subjectDetailsReadModel.ExamSessions = new List<ExamSessionReadModel>();

                    subjectDetailsReadModel.ExamSessions.Add(esrm);
                    return subjectDetailsReadModel;
                },
                param: new { request.Name },
                splitOn: "SubjectName");

            var subject = lookup.GetValueOrDefault(request.Name);
            if (subject is null)
                throw new NotFoundException($"Subject {request.Name} not found");

            return subject;
        }
    }
}
