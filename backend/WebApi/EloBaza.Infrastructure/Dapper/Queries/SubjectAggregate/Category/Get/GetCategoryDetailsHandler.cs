using Dapper;
using EloBaza.Application.Queries.SubjectAggregate.Category.Get;
using EloBaza.Domain.SharedKernel.Exceptions;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.Dapper.Queries.SubjectAggregate.Category.Get
{
    class GetCategoryDetailsHandler : IRequestHandler<GetCategoryDetails, CategoryDetailsReadModel>
    {
        private readonly IDbConnection _dbConnection;

        private const string GetCategoryQuery = @"
SELECT 
    c.CategoryKey AS 'Key',
    c.Name
FROM Category c
WHERE c.CategoryKey = @CategoryKey
";

        public GetCategoryDetailsHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<CategoryDetailsReadModel> Handle(GetCategoryDetails request, CancellationToken cancellationToken)
        {
            var category = await _dbConnection.QuerySingleOrDefaultAsync<CategoryDetailsReadModel>(
                sql: GetCategoryQuery,
                param: new { request.CategoryKey });

            if (category is null)
                throw new NotFoundException($"Category {request.CategoryKey} not found for subject {request.SubjectKey}");

            return category;
        }
    }
}