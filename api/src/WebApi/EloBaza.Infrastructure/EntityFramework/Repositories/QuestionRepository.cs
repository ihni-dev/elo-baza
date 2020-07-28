using EloBaza.Domain.QuestionAggregate;
using EloBaza.Domain.SharedKernel;
using EloBaza.Infrastructure.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EloBaza.Infrastructure.EntityFramework.Repositories
{
    public class QuestionRepository : IRepository<Question>
    {
        private readonly QuestionDbContext _questionDbContext;

        public QuestionRepository(QuestionDbContext questionDbContext)
        {
            _questionDbContext = questionDbContext;
        }

        public async Task<Question> Find(Guid key, CancellationToken cancellationToken)
        {
            return await _questionDbContext
                .Questions
                .FirstOrDefaultAsync(q => q.Key == key, cancellationToken: cancellationToken);
        }

        public async Task Save(Question questionAggregate, CancellationToken cancellationToken)
        {
            if (questionAggregate.IsTransient())
                _questionDbContext.Add(questionAggregate);
            else
                _questionDbContext.Update(questionAggregate);

            await _questionDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
