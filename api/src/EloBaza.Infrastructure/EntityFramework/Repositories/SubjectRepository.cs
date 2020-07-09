using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.Subject;
using EloBaza.Infrastructure.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.EntityFramework.Repositories
{
    public class SubjectRepository : IRepository<SubjectAggregate>
    {
        private readonly SubjectDbContext _subjectDbContext;

        public SubjectRepository(SubjectDbContext subjectDbContext)
        {
            _subjectDbContext = subjectDbContext;
        }

        public async Task<SubjectAggregate> Find(Guid key, CancellationToken cancellationToken)
        {
            return await _subjectDbContext
                .Subjects
                .FirstOrDefaultAsync(s => s.Key == key, cancellationToken: cancellationToken);
        }

        public async Task Save(SubjectAggregate subjectAggregate, CancellationToken cancellationToken)
        {
            if (subjectAggregate.IsTransient())
                _subjectDbContext.Add(subjectAggregate);
            else
                _subjectDbContext.Update(subjectAggregate);

            await _subjectDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
