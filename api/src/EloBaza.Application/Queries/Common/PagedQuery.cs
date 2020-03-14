namespace EloBaza.Application.Queries.Common
{
    public abstract class PagedQuery
    {
        public PagingParameters PagingParameters { get; private set; }

        protected PagedQuery(PagingParameters pagingParameters)
        {
            PagingParameters = pagingParameters;
        }
    }
}
