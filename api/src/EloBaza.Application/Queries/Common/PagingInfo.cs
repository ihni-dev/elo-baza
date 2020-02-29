using System;

namespace EloBaza.Application.Queries.Common
{
    public class PagingInfo
	{
		public int TotalCount { get; private set; }
		public int CurrentPage { get; private set; }
		public int PageSize { get; private set; }
		public int TotalPages { get; private set; }
		public bool HasNext => CurrentPage < TotalPages - 1;
		public bool HasPrevious => CurrentPage > 0;

		public PagingInfo(int totalCount, int currentPage, int pageSize)
		{
			TotalCount = totalCount;
			CurrentPage = currentPage;
			PageSize = pageSize;
			TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
		}
	}
}
