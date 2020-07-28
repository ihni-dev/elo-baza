namespace EloBaza.Application.Queries.Common
{
    public abstract class PagedResult
    {
        public PagingInfo PagingInfo { get; private set; }

        public PagedResult(PagingInfo pagingInfo)
        {
            PagingInfo = pagingInfo;
        }
    }
}
