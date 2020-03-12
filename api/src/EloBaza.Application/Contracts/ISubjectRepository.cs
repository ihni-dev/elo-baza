using EloBaza.Application.Queries.Common;
using EloBaza.Domain;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Contracts
{
    public interface ISubjectRepository
    {
        Task<bool> Exists(Subject subject, CancellationToken cancellationToken = default);
        Task<bool> Exists(string name, CancellationToken cancellationToken = default);
        Task<Subject> Find(Guid id, CancellationToken cancellationToken = default);
        Task<GetAllResult<Subject>> GetAll(Expression<Func<Subject, bool>> condition, PagingParameters pagingParameters, CancellationToken cancellationToken = default);
        Task Add(Subject subject, CancellationToken cancellationToken = default);
        Task Update(Subject subject, CancellationToken cancellationToken = default);
        Task Delete(Subject subject, CancellationToken cancellationToken = default);
    }
}
