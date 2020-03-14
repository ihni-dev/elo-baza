using EloBaza.Application.Queries.Common;
using EloBaza.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Application.Contracts
{
    public interface ISubjectRepository
    {
        Task<bool> Exists(string name, CancellationToken cancellationToken = default);
        Task<Subject> Find(string name, CancellationToken cancellationToken = default);
        void Add(Subject subject);
        void Update(Subject subject);
        void Delete(Subject subject);
        Task SaveChanges(CancellationToken cancellationToken = default);
    }
}
