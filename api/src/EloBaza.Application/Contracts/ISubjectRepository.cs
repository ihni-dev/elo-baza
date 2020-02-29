using EloBaza.Application.Queries.Common;
using EloBaza.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Contracts
{
    public interface ISubjectRepository
    {
        Task<bool> Exists(Subject subject, CancellationToken cancellationToken = default);
        Task<int> GetTotalCount(Expression<Func<Subject, bool>> condition, CancellationToken cancellationToken = default);
        Task<Subject> Find(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Subject>> GetAll(Expression<Func<Subject, bool>> condition, IPagingParameters pagingParameters, CancellationToken cancellationToken = default);
        Task Add(Subject subject, CancellationToken cancellationToken = default);
        Task Update(Subject subject, CancellationToken cancellationToken = default);
    }
}
