using EloBaza.Application.Contracts;
using EloBaza.Domain;
using EloBaza.Infrastructure.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task<bool> Exists(Subject subject)
        {
            return await _subjectDbContext.Subjects.AnyAsync(s => s.Id == subject.Id || s.Name == subject.Name);
        }

        public async Task<Subject> Find(Guid id)
        {
            return await _subjectDbContext.Subjects.FindAsync(id);
        }

        public async Task<IEnumerable<Subject>> GetAll(Func<Subject, bool> condition, int skip, int take)
        {
            return await GetAll()
                .Where(condition)
                .Skip(skip)
                .Take(take)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task Save(Subject subject)
        {
            if (await Exists(subject))
                Update(subject);
            else
                Add(subject);

            await _subjectDbContext.SaveChangesAsync();
        }

        private IQueryable<Subject> GetAll()
        {
            return _subjectDbContext.Subjects.AsQueryable();
        }

        private void Add(Subject subject)
        {
            _subjectDbContext.Subjects.Add(subject);
        }

        private void Update(Subject subject)
        {
            _subjectDbContext.Subjects.Update(subject);
        }
    }
}
