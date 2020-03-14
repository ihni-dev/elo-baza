using EloBaza.Application.Contracts;
using EloBaza.Domain;
using EloBaza.Infrastructure.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.EntityFramework.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SubjectDbContext _subjectDbContext;

        public SubjectRepository(SubjectDbContext subjectDbContext)
        {
            _subjectDbContext = subjectDbContext;
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
                          .Include(s => s.ExamSessions)
                    .SingleOrDefaultAsync(cancellationToken);
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
