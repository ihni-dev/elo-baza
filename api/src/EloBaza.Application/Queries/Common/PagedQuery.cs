using EloBaza.Domain.SharedKernel;

namespace EloBaza.Application.Queries.Common
{
    public abstract class PagedQuery
    {
        public int Skip { get; private set; }
        public int Take { get; private set; }

        protected PagedQuery(int skip, int take)
        {
            using (var validationContext = new ValidationContext())
            {
                if (skip < 0)
                    validationContext.AddError(nameof(Skip), "Skip value must be non-negative number");
                if (take <= 0)
                    validationContext.AddError(nameof(Take), "Take value must be positive number");
            }

            Skip = skip;
            Take = take;
        }
    }
}
