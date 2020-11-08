using Dapper;
using EloBaza.Application.Queries.SubjectAggregate.Category.GetAll;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.Dapper.Queries.SubjectAggregate.Category.GetAll
{
    class GetAllCategoriesHandler : IRequestHandler<GetAllCategories, GetAllCategoriesResult>
    {
        private readonly IDbConnection _dbConnection;

        private const string GetCategoriesQuery = @"
WITH CTE_Recursive (CategoryKey, ParentKey, CategoryId, ParentCategoryId, Name)
AS
(
    SELECT
        c.CategoryKey,
        CAST(NULL AS UNIQUEIDENTIFIER) AS 'ParentKey',
        c.CategoryId,
        c.ParentCategoryId,
        c.Name
    FROM Subject s
    JOIN Category c ON s.SubjectId = c.SubjectId

    UNION ALL
    
    SELECT         
        c.CategoryKey, 
        r.CategoryKey AS 'ParentKey',
        c.CategoryId,
        c.ParentCategoryId,
        c.Name
    FROM Category AS c
    INNER JOIN CTE_Recursive AS r
        ON c.ParentCategoryId = r.CategoryId
)
SELECT  
    c.CategoryKey AS 'Key',
    c.ParentKey AS 'ParentKey',
    c.Name
FROM CTE_Recursive AS c
";

        public GetAllCategoriesHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<GetAllCategoriesResult> Handle(GetAllCategories request, CancellationToken cancellationToken)
        {
            var categories = await _dbConnection.QueryAsync<CategoryReadModel>(
                sql: GetCategoriesQuery,
                param: new { request.SubjectKey });

            return new GetAllCategoriesResult(categories);
        }
    }
}
