namespace EloBaza.Application.Queries.Common
{
    public interface IPagingParameters
    {
        public int Page { get; }
        public int PageSize { get; }
    }
}
