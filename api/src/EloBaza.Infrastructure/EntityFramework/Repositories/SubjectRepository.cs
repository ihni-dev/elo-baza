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

        public async Task<bool> Exists(Subject subject, CancellationToken cancellationToken = default)
        {
            return await _subjectDbContext.Subjects.AnyAsync(s => s.Id == subject.Id || s.Name == subject.Name, cancellationToken);
        }
        public async Task<bool> Exists(string name, CancellationToken cancellationToken = default)
        {
            return await _subjectDbContext.Subjects.AnyAsync(s => s.Name == name, cancellationToken);
        }

        public async Task<int> GetTotalCount(Expression<Func<Subject, bool>> condition, CancellationToken cancellationToken = default)
        {
            return await _subjectDbContext.Subjects
                .Where(condition)
                .CountAsync(cancellationToken);
        }

        public async Task<Subject> Find(Guid id, CancellationToken cancellationToken = default)
        {
            return await _subjectDbContext.Subjects.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<GetAllResult<Subject>> GetAll(Expression<Func<Subject, bool>> condition, IPagingParameters pagingParameters, CancellationToken cancellationToken = default)
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

        public async Task Add(Subject subject, CancellationToken cancellationToken = default)
        {
            _subjectDbContext.Subjects.Add(subject);
            await _subjectDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Subject subject, CancellationToken cancellationToken = default)
        {
            _subjectDbContext.Subjects.Update(subject);
            await _subjectDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Subject subject, CancellationToken cancellationToken = default)
        {
            _subjectDbContext.Subjects.Remove(subject);
            await _subjectDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
