using EloBaza.Application.Contracts;
using EloBaza.Domain;
using EloBaza.Infrastructure.EntityFramework.DbContexts;
using System;
using System.Collections.Generic;
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

        public Task<Subject> Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Subject>> GetAll(int skip, int take, Predicate<Subject> condition)
        {
            throw new NotImplementedException();
        }

        public Task Save(Subject save)
        {
            throw new NotImplementedException();
        }
    }
}
