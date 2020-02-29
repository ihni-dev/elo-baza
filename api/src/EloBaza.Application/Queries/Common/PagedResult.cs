using System.Text.Json.Serialization;

namespace EloBaza.Application.Queries.Common
{
	public abstract class PagedResult
	{
		[JsonIgnore]
		public PagingInfo PagingInfo { get; private set; }

		public PagedResult(PagingInfo pagingInfo)
		{
			PagingInfo = pagingInfo;
		}
	}
}
