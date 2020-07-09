using EloBaza.Domain.SharedKernel.Exceptions;

namespace EloBaza.Application.Queries.Common
{
    public class PagingParameters
    {
        public int Page { get; private set; }
        public int PageSize { get; private set; }

        public PagingParameters(int page, int pageSize)
        {
            using (var validationContext = new ValidationContext())
            {
                validationContext.Validate(() => page <= 0, nameof(Page), "Page Index value must be positive number");
                validationContext.Validate(() => pageSize <= 0, nameof(PageSize), "Page Size value must be positive number");
            }

            Page = page;
            PageSize = pageSize;
        }
    }
}
