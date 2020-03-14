using Dapper;
using EloBaza.Application.Queries.Subject;
using EloBaza.Application.Queries.Subject.Get;
using EloBaza.Domain.SharedKernel;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.Dapper.Queries.Subject.Get
{
    class GetSubjectHandler : IRequestHandler<GetSubject, SubjectReadModel>
    {
        private readonly IDbConnection _dbConnection;

        private const string GetSubjectQuery = @"
SELECT Name
FROM Subject
WHERE Name = @Name
";

        public GetSubjectHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<SubjectReadModel> Handle(GetSubject request, CancellationToken cancellationToken)
        {
            var subject = await _dbConnection.QuerySingleOrDefaultAsync<SubjectReadModel>(GetSubjectQuery, new { request.Name });
            if (subject is null)
                throw new NotFoundException($"Subject {request.Name} not found");

            return subject;
        }
    }
}
