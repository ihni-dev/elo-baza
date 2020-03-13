using EloBaza.Application.Contracts;
using EloBaza.Application.Queries.Common;
using EloBaza.Domain;
using EloBaza.Infrastructure.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.EntityFramework.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SubjectDbContext _subjectDbContext;
        private readonly ILogger<SubjectRepository> _logger;

        public SubjectRepository(SubjectDbContext subjectDbContext, ILogger<SubjectRepository> logger)
        {
            _subjectDbContext = subjectDbContext;
            _logger = logger;
        }

        public async Task<bool> Exists(string name, CancellationToken cancellationToken = default)
        {
            return await (from subject in _subjectDbContext.Subjects
                          where subject.Name == name
                          select 1)
                    .AnyAsync(cancellationToken);
        }

        public async Task<Subject> Find(string name, CancellationToken cancellationToken = default)
        {
            return await (from subject in _subjectDbContext.Subjects
                          where subject.Name == name
                          select subject)
                    .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<GetAllResult<Subject>> GetAll(Expression<Func<Subject, bool>> condition, PagingParameters pagingParameters, CancellationToken cancellationToken = default)
        {
            var data = await _subjectDbContext.Subjects
                .Where(condition)
                .Skip((pagingParameters.Page - 1) * pagingParameters.PageSize)
                .Take(pagingParameters.PageSize)
                .ToListAsync(cancellationToken);

            var totalCount = await _subjectDbContext.Subjects
                .Where(condition)
                .CountAsync();

            return new GetAllResult<Subject>(data, totalCount);
        }

        public void Add(Subject subject)
        {
            _subjectDbContext.Subjects.Add(subject);
        }

        public void Update(Subject subject)
        {
            _subjectDbContext.Subjects.Update(subject);
        }

        public void Delete(Subject subject)
        {
            _subjectDbContext.Subjects.Remove(subject);
        }

        public async Task SaveChanges(CancellationToken cancellationToken = default)
        {
            await _subjectDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
