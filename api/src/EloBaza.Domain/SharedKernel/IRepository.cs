using System;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Domain.SharedKernel
{
    public interface IRepository<T> where T : AggregateRoot
    {
        Task<T> Find(Guid key, CancellationToken cancellationToken);
        Task Save(T aggregate, CancellationToken cancellationToken);
    }
}
