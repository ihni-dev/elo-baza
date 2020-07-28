using System;

namespace EloBaza.Application.Queries.Common
{
    /// <summary>
    /// Information about queried resource pagination
    /// </summary>
    public class PagingInfo
    {
        /// <summary>
        /// Total number of records
        /// </summary>
        public int TotalCount { get; private set; }
        /// <summary>
        /// Current page number
        /// </summary>
        public int Page { get; private set; }
        /// <summary>
        /// Current page size
        /// </summary>
        public int PageSize { get; private set; }
        /// <summary>
        /// Last page number
        /// </summary>
        public int LastPage { get; private set; }
        public bool HasNext => Page < LastPage;
        public bool HasPrevious => Page > 1;

        public PagingInfo(int totalCount, int page, int pageSize)
        {
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
            LastPage = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
