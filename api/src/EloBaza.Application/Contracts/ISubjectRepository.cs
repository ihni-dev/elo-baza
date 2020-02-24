using EloBaza.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EloBaza.Application.Contracts
{
    public interface ISubjectRepository
    {
        Task<Subject> Find(Guid id);
        Task<IEnumerable<Subject>> GetAll(int skip, int take, Predicate<Subject> condition);
        Task Save(Subject save);
    }
}
