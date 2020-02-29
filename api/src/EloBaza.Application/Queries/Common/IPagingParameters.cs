namespace EloBaza.Application.Queries.Common
{
    public interface IPagingParameters
    {
        public int PageIndex { get; }
        public int PageSize { get; }
    }
}
