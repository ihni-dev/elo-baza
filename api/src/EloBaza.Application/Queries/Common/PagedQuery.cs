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
                validationContext.Validate(() => pagingParameters.Page > 0, nameof(pagingParameters.Page), "Page Index value must be positive number");
                validationContext.Validate(() => pagingParameters.PageSize > 0, nameof(pagingParameters.PageSize), "Page Size value must be positive number");
            }

            PagingParameters = pagingParameters;
        }
    }
}
