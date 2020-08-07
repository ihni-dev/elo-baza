using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.UserAggregate;
using EloBaza.Infrastructure.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.EntityFramework.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly UserDbContext _userDbContext;

        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<User> Find(Guid key, CancellationToken cancellationToken)
        {
            return await _userDbContext
                .Users
                .FirstOrDefaultAsync(q => q.Key == key, cancellationToken: cancellationToken);
        }

        public async Task Save(User userAggregate, CancellationToken cancellationToken)
        {
            if (userAggregate.IsTransient())
                _userDbContext.Add(userAggregate);
            else
                _userDbContext.Update(userAggregate);

            await _userDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
