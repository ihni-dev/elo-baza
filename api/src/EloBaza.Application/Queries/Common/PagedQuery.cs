using EloBaza.Domain.SharedKernel;

namespace EloBaza.Application.Queries.Common
{
    public abstract class PagedQuery
    {
        public IPagingParameters PagingParameters { get; private set; }

        protected PagedQuery(IPagingParameters pagingParameters)
        {
            using (var validationContext = new ValidationContext())
            {
                if (pagingParameters.PageIndex < 0)
                    validationContext.AddError(nameof(pagingParameters.PageIndex), "Page Index value must be non-negative number");
                if (pagingParameters.PageSize <= 0)
                    validationContext.AddError(nameof(pagingParameters.PageSize), "Page Size value must be positive number");
            }

            PagingParameters = pagingParameters;
        }
    }
}
